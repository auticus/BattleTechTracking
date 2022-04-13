﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Factories;
using BattleTechTracking.Models;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class MatchViewModel : BaseViewModel
    {
        private const int FACTION_1_INDEX = 0;
        private const int FACTION_2_INDEX = 1;
        
        private readonly List<BattleMech> _mechList;
        private readonly List<IndustrialUnit> _industrialMechList;
        private readonly List<Infantry> _infantryList;
        private readonly List<CombatVehicle> _combatVehicleList;

        private int _activeFaction;
        private bool _settingsVisible;
        private bool _unitSelectorVisible;
        private string _activeFactionName;
        private string _faction1Name;
        private string _faction2Name;
        private readonly List<IDisplayListView>[] _factionUnits = {new List<IDisplayListView>(), new List<IDisplayListView>()};
        private IDisplayListView _selectedActiveUnit;

        /// <summary>
        /// Gets or sets the active faction index.
        /// </summary>
        public int ActiveFaction
        {
            get => _activeFaction;
            set
            {
                _activeFaction = value;
                OnPropertyChanged(nameof(ActiveFaction));
                ActiveFactionUnits = new ObservableCollection<IDisplayListView>(_factionUnits[ActiveFaction]);
                ActiveFactionName = ActiveFaction == 0 ? Faction1Name : Faction2Name;
            }
        }

        public string ActiveFactionName
        {
            get => _activeFactionName;
            set
            {
                _activeFactionName = value;
                OnPropertyChanged(nameof(ActiveFactionName));
            }
        }

        public string Faction1Name
        {
            get => _faction1Name;
            set
            {
                _faction1Name = value;
                OnPropertyChanged(nameof(Faction1Name));
            }
        }

        public string Faction2Name
        {
            get => _faction2Name;
            set
            {
                _faction2Name = value;
                OnPropertyChanged(nameof(Faction2Name));
            }
        }

        public ObservableCollection<IDisplayListView> ActiveFactionUnits { get; private set; }

        public IDisplayListView SelectedActiveUnit
        {
            get => _selectedActiveUnit;
            set
            {
                _selectedActiveUnit = value;
                OnPropertyChanged(nameof(SelectedActiveUnit));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the Settings panel is visible.
        /// </summary>
        public bool SettingsVisible
        {
            get => _settingsVisible;
            set
            {
                _settingsVisible = value;
                OnPropertyChanged(nameof(SettingsVisible));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the Unit Selector is visible.
        /// </summary>
        public bool UnitSelectorVisible
        {
            get => _unitSelectorVisible;
            set
            {
                _unitSelectorVisible = value;
                OnPropertyChanged(nameof(UnitSelectorVisible));
            }
        }

        /// <summary>
        /// Gets the unit filters from the unit selector.
        /// </summary>
        public ObservableCollection<string> UnitFilters { get; }

        private ObservableCollection<IDisplayListView> _visibleUnits;
        public ObservableCollection<IDisplayListView> VisibleUnits
        {
            get => _visibleUnits;
            private set
            {
                _visibleUnits = value;
                OnPropertyChanged(nameof(VisibleUnits));
                SelectedUnit = null;
            }
        }

        private IDisplayListView _selectedUnit;

        /// <summary>
        /// Gets or sets the Selected Unit from the Unit Selector
        /// </summary>
        public IDisplayListView SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }

        private string _selectedUnitFilter;
        /// <summary>
        /// Gets or sets the value that will filter the List View by what type of item to look at.
        /// </summary>
        public string SelectedUnitFilter
        {
            get => _selectedUnitFilter;
            set
            {
                if (_selectedUnitFilter == value) return;
                _selectedUnitFilter = value;
                OnPropertyChanged(nameof(SelectedUnitFilter));
                LoadVisibleUnits();
            }
        }

        public ICommand SettingsCommand { get; }
        public ICommand SettingsOkCommand { get; }
        public ICommand OkCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ActivateFaction1Command { get; }
        public ICommand ActivateFaction2Command { get; }
        public ICommand AddUnits { get; }
        public ICommand SelectorOkCommand { get; }

        public MatchViewModel()
        {
            Faction1Name = "Faction 1";
            Faction2Name = "Faction 2";
            ActiveFaction = 0;

            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            VisibleUnits = new ObservableCollection<IDisplayListView>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();
            _industrialMechList = DataPump.GetPersistedDataForType<IndustrialUnit>().ToList();
            _infantryList = DataPump.GetPersistedDataForType<Infantry>().ToList();
            _combatVehicleList = DataPump.GetPersistedDataForType<CombatVehicle>().ToList();

            OkCommand = new Command(() =>
            {
                //todo: save code goes here
                PageNavigation.PopAsync();
            });

            CloseCommand = new Command(() =>
            {
                PageNavigation.PopAsync();
            });

            ActivateFaction1Command = new Command(() =>
            {
                ActiveFaction = FACTION_1_INDEX;
            });

            ActivateFaction2Command = new Command(() =>
            {
                ActiveFaction = FACTION_2_INDEX;
            });

            AddUnits = new Command(() =>
            {
                SettingsVisible = false;
                UnitSelectorVisible = true;
            });

            SettingsCommand = new Command(() =>
            {
                UnitSelectorVisible = false;
                SettingsVisible = true;
            });

            SettingsOkCommand = new Command(() =>
            {
                SettingsVisible = false;
            });

            SelectorOkCommand = new Command(() =>
            {
                UnitSelectorVisible = false;
            });
        }

        private void LoadVisibleUnits()
        {
            VisibleUnits = new ObservableCollection<IDisplayListView>(GetAssociatedUnitsByFilterType());
        }

        private IEnumerable<IDisplayListView> GetAssociatedUnitsByFilterType()
        {
            switch (SelectedUnitFilter)
            {
                case UnitTypes.BATTLE_MECH:
                    return _mechList.OrderBy(p => p.Name).ThenBy(p => p.Model);
                case UnitTypes.INDUSTRIAL_MECH:
                    return _industrialMechList.OrderBy(p => p.Name).ThenBy(p => p.Model);
                case UnitTypes.INFANTRY:
                    return new List<IDisplayListView>();
                //return _infantryList.OrderBy(p => p.Name).ThenBy(p => p.Weapon);
                //infantry is not a display unit
                case UnitTypes.COMBAT_VEHICLE:
                    return _combatVehicleList.OrderBy(p => p.Name).ThenBy(p => p.Model);
                default:
                    throw new NotImplementedException($"The selected unit type {SelectedUnitFilter} does not exist");
            }
        }
    }
}
