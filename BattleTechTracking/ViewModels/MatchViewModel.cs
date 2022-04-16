using System;
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
        private readonly List<IndustrialMech> _industrialMechList;
        private readonly List<Infantry> _infantryList;
        private readonly List<CombatVehicle> _combatVehicleList;

        private int _activeFaction;
        private bool _settingsVisible;
        private bool _unitSelectorVisible;
        private bool _matchTrackingViewVisible;
        private bool _activeComponentsVisible;
        private bool _activeUnitEquipmentVisible;
        private bool _activeUnitWeaponsVisible;
        private bool _activeUnitAmmoVisible;
        private string _activeFactionName;
        private string _faction1Name;
        private string _faction2Name;
        private readonly List<IDisplayListView>[] _factionUnits = {new List<IDisplayListView>(), new List<IDisplayListView>()};
        private ObservableCollection<IDisplayListView> _activeFactionUnits;
        private ObservableCollection<UnitComponent> _activeUnitComponents;
        private ObservableCollection<Equipment> _activeUnitEquipment;
        private ObservableCollection<Weapon> _activeUnitWeapons;
        private ObservableCollection<Ammunition> _activeUnitAmmunition;
        private TrackedGameElement _selectedActiveUnit;

        /// <summary>
        /// Gets or sets the active faction index.
        /// </summary>
        public int ActiveFaction
        {
            get => _activeFaction;
            set
            {
                //put the current active observable collection into the cache
                if (ActiveFactionUnits != null) _factionUnits[ActiveFaction] = ActiveFactionUnits.ToList();

                _activeFaction = value;
                OnPropertyChanged(nameof(ActiveFaction));

                //make the new active observable collection based on the new active faction
                ActiveFactionUnits = new ObservableCollection<IDisplayListView>(_factionUnits[ActiveFaction]);
                ActiveFactionName = ActiveFaction == 0 ? Faction1Name : Faction2Name;

                // this is a turbo hack - but UWP for whatever reason gets really angry if there is only one element that gets set null
                // it no longer registers selections and the add button stops working
                SelectedActiveUnit = ActiveFactionUnits.Count == 1 ? (TrackedGameElement)ActiveFactionUnits[0] : null;
                MatchTrackingViewVisible = SelectedActiveUnit != null;
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

                if (ActiveFaction == 0) ActiveFactionName = value;
            }
        }

        public string Faction2Name
        {
            get => _faction2Name;
            set
            {
                _faction2Name = value;
                OnPropertyChanged(nameof(Faction2Name));

                if (ActiveFaction == 1) ActiveFactionName = value;
            }
        }

        /// <summary>
        /// The item source of the active faction's list of units.
        /// </summary>
        public ObservableCollection<IDisplayListView> ActiveFactionUnits
        {
            get => _activeFactionUnits;
            set
            {
                _activeFactionUnits = value;
                OnPropertyChanged(nameof(ActiveFactionUnits));
            }
        }

        public ObservableCollection<UnitComponent> ActiveUnitComponents
        {
            get => _activeUnitComponents;
            set
            {
                _activeUnitComponents = value;
                OnPropertyChanged(nameof(ActiveUnitComponents));
            }
        }

        public ObservableCollection<Equipment> ActiveUnitEquipment
        {
            get => _activeUnitEquipment;
            set
            {
                _activeUnitEquipment = value;
                OnPropertyChanged(nameof(ActiveUnitEquipment));
            }
        }

        public ObservableCollection<Weapon> ActiveUnitWeapons
        {
            get => _activeUnitWeapons;
            set
            {
                _activeUnitWeapons = value;
                OnPropertyChanged(nameof(ActiveUnitWeapons));
            }
        }

        public ObservableCollection<Ammunition> ActiveUnitAmmunition
        {
            get => _activeUnitAmmunition;
            set
            {
                _activeUnitAmmunition = value;
                OnPropertyChanged(nameof(ActiveUnitAmmunition));
            }
        }

        /// <summary>
        /// Gets or sets the selected faction unit.
        /// </summary>
        public TrackedGameElement SelectedActiveUnit
        {
            get => _selectedActiveUnit;
            set
            {
                _selectedActiveUnit = value;
                OnPropertyChanged(nameof(SelectedActiveUnit));

                if (_selectedActiveUnit != null)
                {
                    SetAllPanelsInvisible();
                    MatchTrackingViewVisible = true;
                }
                if (_selectedActiveUnit == null)
                {
                    ActiveUnitComponents = null;
                    ActiveUnitEquipment = null;
                    ActiveUnitWeapons = null;
                    ActiveUnitAmmunition = null;
                }
                else
                {
                    ActiveUnitComponents = new ObservableCollection<UnitComponent>(_selectedActiveUnit.UnitComponents.ToList());
                    ActiveUnitEquipment = new ObservableCollection<Equipment>(_selectedActiveUnit.UnitEquipment.ToList());
                    ActiveUnitWeapons = new ObservableCollection<Weapon>(_selectedActiveUnit.UnitWeapons.ToList());
                    ActiveUnitAmmunition = new ObservableCollection<Ammunition>(_selectedActiveUnit.UnitAmmunition.ToList());
                }
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

        public bool MatchTrackingViewVisible
        {
            get => _matchTrackingViewVisible;
            set
            {
                _matchTrackingViewVisible = value;
                OnPropertyChanged(nameof(MatchTrackingViewVisible));
            }
        }

        public bool ActiveComponentsVisible
        {
            get => _activeComponentsVisible;
            set
            {
                _activeComponentsVisible = value;
                OnPropertyChanged(nameof(ActiveComponentsVisible));
            }
        }

        public bool ActiveUnitEquipmentVisible
        {
            get => _activeUnitEquipmentVisible;
            set
            {
                _activeUnitEquipmentVisible = value;
                OnPropertyChanged(nameof(ActiveUnitEquipmentVisible));
            }
        }

        public bool ActiveUnitWeaponsVisible
        {
            get => _activeUnitWeaponsVisible;
            set
            {
                _activeUnitWeaponsVisible = value;
                OnPropertyChanged(nameof(ActiveUnitWeaponsVisible));
            }
        }

        public bool ActiveUnitAmmoVisible
        {
            get => _activeUnitAmmoVisible;
            set
            {
                _activeUnitAmmoVisible = value;
                OnPropertyChanged(nameof(ActiveUnitAmmoVisible));
            }
        }

        /// <summary>
        /// Gets the unit filters from the unit selector.
        /// </summary>
        public ObservableCollection<string> UnitFilters { get; }

        private ObservableCollection<IDisplayListView> _selectorViewVisibleUnits;
        public ObservableCollection<IDisplayListView> SelectorViewVisibleUnits
        {
            get => _selectorViewVisibleUnits;
            private set
            {
                _selectorViewVisibleUnits = value;
                OnPropertyChanged(nameof(SelectorViewVisibleUnits));
                SelectorViewSelectedUnit = null;
            }
        }

        private IDisplayListView _selectorViewSelectedUnit;

        /// <summary>
        /// Gets or sets the Selected Unit from the Unit Selector
        /// </summary>
        public IDisplayListView SelectorViewSelectedUnit
        {
            get => _selectorViewSelectedUnit;
            set
            {
                _selectorViewSelectedUnit = value;
                OnPropertyChanged(nameof(SelectorViewSelectedUnit));
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

        /// <summary>
        /// Gets the command to open the Settings view.
        /// </summary>
        public ICommand SettingsCommand { get; }

        /// <summary>
        /// Gets the Ok command for the Settings control.
        /// </summary>
        public ICommand SettingsOkCommand { get; }

        /// <summary>
        /// Gets the Ok Command for the Match View screen which should save and close to main screen.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Gets the Close command for the Match View screen which should simply close to the main screen.
        /// </summary>
        public ICommand CloseCommand { get; }

        public ICommand ActivateFaction1Command { get; }
        public ICommand ActivateFaction2Command { get; }
        
        /// <summary>
        /// Gets the command to add a unit to the selected faction.
        /// </summary>
        public ICommand AddUnits { get; }

        /// <summary>
        /// Gets the command to delete a unit from the active faction.
        /// </summary>
        public ICommand DeleteUnit { get; }

        /// <summary>
        /// Gets the ok command for the unit selection view.
        /// </summary>
        public ICommand SelectorOkCommand { get; }

        /// <summary>
        /// Gets the command to start a new round.
        /// </summary>
        public ICommand BeginNewRound { get; }

        public ICommand ViewTrackGameElementDetails { get; }
        public ICommand ViewActiveUnitComponents { get; }
        public ICommand ViewActiveUnitEquipment { get; }
        public ICommand ViewActiveUnitWeapons { get; }
        public ICommand ViewActiveUnitAmmo { get; }
        public ICommand DeleteComponent { get; }

        public MatchViewModel()
        {
            Faction1Name = "Faction 1";
            Faction2Name = "Faction 2";
            ActiveFaction = 0;

            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            SelectorViewVisibleUnits = new ObservableCollection<IDisplayListView>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();
            _industrialMechList = DataPump.GetPersistedDataForType<IndustrialMech>().ToList();
            _infantryList = DataPump.GetPersistedDataForType<Infantry>().ToList();
            _combatVehicleList = DataPump.GetPersistedDataForType<CombatVehicle>().ToList();

            SelectedUnitFilter = UnitTypes.BATTLE_MECH;

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
                SetAllPanelsInvisible();
                UnitSelectorVisible = true;
            });

            DeleteUnit = new Command<Guid>((id) =>
            {
                var unit = ActiveFactionUnits.FirstOrDefault(x => x.ID == id);
                if (unit == null) return;
                ActiveFactionUnits.Remove(unit);
                SelectedActiveUnit = null;
            });

            SettingsCommand = new Command(() =>
            {
                SetAllPanelsInvisible();
                SettingsVisible = true;
            });

            SettingsOkCommand = new Command(() =>
            {
                SettingsVisible = false;
                if (SelectedActiveUnit != null) MatchTrackingViewVisible = true;
            });

            SelectorOkCommand = new Command(() =>
            {
                UnitSelectorVisible = false;
                if (SelectedActiveUnit != null) MatchTrackingViewVisible = true;
                AddUnitToActiveFaction();
            });

            BeginNewRound = new Command(() =>
            {
                var nonActiveFaction = ActiveFaction == 1 ? 0 : 1;
                foreach (var element in _factionUnits[nonActiveFaction])
                {
                    ((TrackedGameElement)element).NextRound();
                }

                foreach (var element in ActiveFactionUnits)
                {
                    ((TrackedGameElement)element).NextRound();
                }
            });

            ViewTrackGameElementDetails = new Command(() =>
            {
                SetAllPanelsInvisible();
                MatchTrackingViewVisible = true;
            });

            ViewActiveUnitComponents = new Command(() =>
            {
                SetAllPanelsInvisible();
                ActiveComponentsVisible = true;
            });

            ViewActiveUnitEquipment = new Command(() =>
            {
                SetAllPanelsInvisible();
                ActiveUnitEquipmentVisible = true;
            });

            ViewActiveUnitWeapons = new Command(() =>
            {
                SetAllPanelsInvisible();
                ActiveUnitWeaponsVisible = true;
            });

            ViewActiveUnitAmmo = new Command(() =>
            {
                SetAllPanelsInvisible();
                ActiveUnitAmmoVisible = true;
            });
        }

        private void SetAllPanelsInvisible()
        {
            UnitSelectorVisible = false;
            SettingsVisible = false;
            MatchTrackingViewVisible = false;
            ActiveComponentsVisible = false;
            ActiveUnitEquipmentVisible = false;
            ActiveUnitWeaponsVisible = false;
            ActiveUnitAmmoVisible = false;
        }

        private void LoadVisibleUnits()
        {
            SelectorViewVisibleUnits = new ObservableCollection<IDisplayListView>(GetAssociatedUnitsByFilterType());
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
                    return _infantryList.OrderBy(p => p.Name).ThenBy(p => p.Weapon);
                case UnitTypes.COMBAT_VEHICLE:
                    return _combatVehicleList.OrderBy(p => p.Name).ThenBy(p => p.Model);
                default:
                    throw new NotImplementedException($"The selected unit type {SelectedUnitFilter} does not exist");
            }
        }

        private void AddUnitToActiveFaction()
        {
            switch (SelectorViewSelectedUnit)
            {
                case null:
                    return;
                case IndustrialMech _:
                    ActiveFactionUnits.Add(MechFactory.BuildTrackedGameElement((IndustrialMech)SelectorViewSelectedUnit));
                    return;
                case BattleMech _:
                    ActiveFactionUnits.Add(MechFactory.BuildTrackedGameElement((BattleMech)SelectorViewSelectedUnit));
                    return;
                case CombatVehicle _:
                    ActiveFactionUnits.Add(CombatVehicleFactory.BuildTrackedGameElement((CombatVehicle)SelectorViewSelectedUnit));
                    return;
                case Infantry _:
                    ActiveFactionUnits.Add(InfantryFactory.BuildTrackedGameElement((Infantry)SelectorViewSelectedUnit));
                    return;
            }
        }
    }
}
