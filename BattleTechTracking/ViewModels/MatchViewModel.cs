using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Factories;
using BattleTechTracking.Models;
using BattleTechTracking.Reports;
using BattleTechTracking.Utilities;
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
        private bool _dataReportsVisible;
        private bool _playerOneFactionVisible;
        private bool _playerTwoFactionVisible;
        private bool _batchUpdateActive = false;
        private string _activeFactionName;
        private string _faction1Name;
        private string _faction2Name;
        private string _unitNameFilter;
        private string _selectedUnitFilter;
        private IDisplayListView _selectorViewSelectedUnit;

        private readonly Dictionary<int, ObservableCollection<GroupedGameElement>> _factionUnits =
            new Dictionary<int, ObservableCollection<GroupedGameElement>>();

        private ObservableCollection<IDisplayListView> _selectorViewVisibleUnits;
        private TrackedGameElement _selectedActiveUnit;

        /// <summary>
        /// Gets or sets the active faction index.
        /// </summary>
        public int ActiveFaction
        {
            get => _activeFaction;
            set
            {
                _activeFaction = value;

                PlayerOneFactionVisible = ActiveFaction == 0;
                PlayerTwoFactionVisible = ActiveFaction == 1;

                ActiveFactionName = ActiveFaction == 0 ? Faction1Name : Faction2Name;
                
                // this is a turbo hack - but UWP for whatever reason gets really angry if there is only one element that gets set null
                // it no longer registers selections and the add button stops working
                var activeUnits = GetActiveFaction();
                SelectedActiveUnit = activeUnits.Count == 1
                    ? (TrackedGameElement)activeUnits[0].GameElements[0]
                    : null;
                MatchTrackingViewVisible = SelectedActiveUnit != null;
                OnPropertyChanged(nameof(ActiveFaction));
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
        public ObservableCollection<GroupedGameElement> PlayerOneUnits
        {
            get => _factionUnits[0];
        }

        public ObservableCollection<GroupedGameElement> PlayerTwoUnits
        {
            get => _factionUnits[1];
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
                SetActiveFlagOnSelectedElement();
                SetDefaultPanelVisible();
            }
        }

        public bool PlayerOneFactionVisible
        {
            get => _playerOneFactionVisible;
            set
            {
                _playerOneFactionVisible = value;
                OnPropertyChanged(nameof(PlayerOneFactionVisible));
            }
        }

        public bool PlayerTwoFactionVisible
        {
            get => _playerTwoFactionVisible;
            set
            {
                _playerTwoFactionVisible = value;
                OnPropertyChanged(nameof(PlayerTwoFactionVisible));
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

        public bool DataReportsVisible
        {
            get =>  _dataReportsVisible;
            set
            {
                _dataReportsVisible = value;
                OnPropertyChanged(nameof(DataReportsVisible));
            }
        }

        /// <summary>
        /// Gets the unit filters from the unit selector.
        /// </summary>
        public ObservableCollection<string> UnitFilters { get; }

        public List<string> UnitActions { get; }
        public List<string> UnitStatuses { get; }

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

        public string UnitNameFilter
        {
            get => _unitNameFilter;
            set
            {
                _unitNameFilter = value;
                OnPropertyChanged(nameof(UnitNameFilter));
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
        /// Gets the Save Command for the Match View screen which should open the save view to select a file name.
        /// </summary>
        public ICommand SaveCommand { get; }

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

        /// <summary>
        /// Gets the command to display the reports screen.
        /// </summary>
        public ICommand ReportsCommand { get; }

        /// <summary>
        /// Gets the command to fire a given weapon.
        /// </summary>
        public ICommand FireWeaponCommand { get; }

        public ICommand ViewTrackGameElementDetails { get; }
        public ICommand ViewActiveUnitComponents { get; }
        public ICommand ViewActiveUnitEquipment { get; }
        public ICommand ViewActiveUnitWeapons { get; }
        public ICommand ViewActiveUnitAmmo { get; }
        public ICommand GenerateHeat1 { get; }
        public ICommand GenerateHeat2 { get; }
        public ICommand GenerateHeat5 { get; }

        public ICommand FilterUnits { get; }

        public DataReportViewModel DataReportVM { get; } = new DataReportViewModel();
        public SaveFileViewModel SaveFileVM { get; } = new SaveFileViewModel();

        public MatchViewModel()
        {
            InitializeFactionUnitData();

            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            UnitActions = ActionsFactory.BuildActionsList().ToList();
            UnitStatuses = UnitStatusFactory.BuildStatusList().ToList();
            SelectorViewVisibleUnits = new ObservableCollection<IDisplayListView>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();
            _industrialMechList = DataPump.GetPersistedDataForType<IndustrialMech>().ToList();
            _infantryList = DataPump.GetPersistedDataForType<Infantry>().ToList();
            _combatVehicleList = DataPump.GetPersistedDataForType<CombatVehicle>().ToList();

            SelectedUnitFilter = UnitTypes.BATTLE_MECH;

            SaveFileVM.OnFileViewModelSaved += (sender, args) =>
            {
                SetAllPanelsInvisible();
                MatchTrackingViewVisible = true;
            };

            SaveCommand = new Command(() =>
            {
                const int factionOneId = 0;
                const int factionTwoId = 1;
                var matchState = new MatchState()
                {
                    Faction1Name = this.Faction1Name,
                    Faction2Name = this.Faction2Name,
                    Factions = new IList<IDisplayMatchedListView>[]{FlattenFactionUnitsToList(factionOneId), FlattenFactionUnitsToList(factionTwoId)}
                };

                SaveFileVM.SavedMatchState = matchState;
                SetAllPanelsInvisible();
                SaveFileVM.SaveFileIsVisible = true;
            });

            CloseCommand = new Command(() => { PageNavigation.PopAsync(); });

            ActivateFaction1Command = new Command(() => { ActiveFaction = FACTION_1_INDEX; });

            ActivateFaction2Command = new Command(() => { ActiveFaction = FACTION_2_INDEX; });

            AddUnits = new Command(() =>
            {
                SetAllPanelsInvisible();
                UnitSelectorVisible = true;
            });

            DeleteUnit = new Command<Guid>((id) =>
            {
                RemoveUnitFromCollectionByID(id);
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
                var units = new List<ITrackable>();
                for (var factionID = 0; factionID < _factionUnits.Count; factionID++)
                {
                    foreach (var groupedElement in _factionUnits[factionID])
                    {
                        units.AddRange(groupedElement.GameElements.Cast<ITrackable>());
                    }
                }

                _batchUpdateActive = true;
                GameStateTracker.NextRound(units);

                RefreshObservbableCollection(FACTION_1_INDEX);
                RefreshObservbableCollection(FACTION_2_INDEX);
                _batchUpdateActive = false;
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
                GetUpdatedTargetModifiers();
                ActiveUnitWeaponsVisible = true;
            });

            ViewActiveUnitAmmo = new Command(() =>
            {
                SetAllPanelsInvisible();
                ActiveUnitAmmoVisible = true;
            });

            ReportsCommand = new Command(() =>
            {
                var factionUnitData = new IList<IDisplayMatchedListView>[_factionUnits.Count];
                for (var factionID = 0; factionID < _factionUnits.Count; factionID++)
                {
                    factionUnitData[factionID] = FlattenFactionUnitsToList(factionID);
                }

                DataReportVM.RefreshReportData(new TextReportInput(TextReportInput.ConvertFactionDataToReportableFormat(factionUnitData),
                                                            new string[]{Faction1Name, Faction2Name}));
                DataReportVM.TextReportContents = string.Empty;
                DataReportVM.SelectedReport = string.Empty;

                SetAllPanelsInvisible();
                DataReportsVisible = true;
            });

            FireWeaponCommand = new Command<Guid>((id) =>
            {
                var wpn = SelectedActiveUnit.UnitWeapons.FirstOrDefault(weapon => weapon.ID == id);
                if (wpn == null) return;
                if (wpn.WeaponFiringStatus != WeaponFiringStatus.NotFired) return; //if the status is set, cannot fire again
                if (wpn.WeaponFiringStatus == WeaponFiringStatus.WeaponDestroyed) return;

                if (!wpn.UsesAmmunition)
                {
                    wpn.WeaponFiringStatus = WeaponFiringStatus.WeaponFired;
                    SelectedActiveUnit.CurrentHeatLevel += wpn.Heat;
                    return;
                }

                //find the ammo and if so, remove one otherwise return no ammo
                var allAmmo = SelectedActiveUnit.UnitAmmunition.Where(x => x.Name.Contains(wpn.Name));
                foreach (var ammo in allAmmo)
                {
                    if (ammo.AmmoCount == 0 || ammo.Location == EquipmentStatus.DESTROYED) continue;
                    ammo.AmmoCount--;
                    SelectedActiveUnit.CurrentHeatLevel += wpn.Heat;
                    wpn.WeaponFiringStatus = WeaponFiringStatus.WeaponFired;
                    return;
                }

                wpn.WeaponFiringStatus = WeaponFiringStatus.OutOfAmmo;
            });

            GenerateHeat1 = new Command(() =>
            {
                SelectedActiveUnit.CurrentHeatLevel++;
            });
            GenerateHeat2 = new Command(() =>
            {
                SelectedActiveUnit.CurrentHeatLevel += 2;
            });
            GenerateHeat5 = new Command(() =>
            {
                SelectedActiveUnit.CurrentHeatLevel += 5;
            });

            FilterUnits = new Command(FilterVisibleUnitsBySearchCondition);
        }

        /// <summary>
        /// Called externally to pass in match state to the view model to set the data elements up.
        /// </summary>
        /// <param name="state"></param>
        public void LoadMatchState(MatchState state)
        {
            if (state == null) return;

            Faction1Name = state.Faction1Name;
            Faction2Name = state.Faction2Name;

            _factionUnits.Clear();

            for (var factionID = 0; factionID < state.Factions.Length; factionID++)
            {
                foreach (var item in state.Factions[factionID])
                {
                    item.Invalidated += OnGroupedElement_Invalidated;
                }

                var faction = GetGroupedFactionDataFromSavedMatchState(state.Factions[factionID]);
                _factionUnits.Add(factionID, faction);
            }

            OnPropertyChanged(nameof(PlayerOneUnits));
            OnPropertyChanged(nameof(PlayerTwoUnits));
            PlayerOneFactionVisible = true;
            PlayerTwoFactionVisible = false;
        }

        private void InitializeFactionUnitData()
        {
            Faction1Name = "Faction 1";
            Faction2Name = "Faction 2";
            _factionUnits.Add(0, new ObservableCollection<GroupedGameElement>());
            _factionUnits.Add(1, new ObservableCollection<GroupedGameElement>());
            ActiveFaction = 0;

            OnPropertyChanged(nameof(PlayerOneUnits));
            OnPropertyChanged(nameof(PlayerTwoUnits));
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
            DataReportsVisible = false;
            SaveFileVM.SaveFileIsVisible = false;
        }

        private void LoadVisibleUnits()
        {
            SelectorViewVisibleUnits = new ObservableCollection<IDisplayListView>(GetAssociatedUnitsByFilterType());
        }

        private void FilterVisibleUnitsBySearchCondition()
        {
            //fires off whenever the user clicks the magnifying glass icon to filter down whatever is currently loaded
            LoadVisibleUnits();
            if (string.IsNullOrEmpty(UnitNameFilter)) return;

            var filteredList = SelectorViewVisibleUnits.Where(unit => unit.UnitHeader.Contains(UnitNameFilter)).ToList();
            SelectorViewVisibleUnits = new ObservableCollection<IDisplayListView>(filteredList);
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
            TrackedGameElement gameElement = null;

            switch (SelectorViewSelectedUnit)
            {
                case null:
                    return;
                case IndustrialMech _:
                    gameElement = MechFactory.BuildTrackedGameElement((IndustrialMech)SelectorViewSelectedUnit);
                    break;
                case BattleMech _:
                    gameElement = MechFactory.BuildTrackedGameElement((BattleMech)SelectorViewSelectedUnit);
                    break;
                case CombatVehicle _:
                    gameElement = CombatVehicleFactory.BuildTrackedGameElement((CombatVehicle)SelectorViewSelectedUnit);
                    break;
                case Infantry _:
                    gameElement = InfantryFactory.BuildTrackedGameElement((Infantry)SelectorViewSelectedUnit);
                    break;
                default:
                    throw new NotImplementedException(
                        "The type of the game element passed is not supported in MatchViewModel::AddUnitToActiveFaction");
            }

            // pull the grouping out of the ActionFactionUnits collection - and if it doesn't exist right now - add that category
            var activeFactionUnits = GetActiveFaction().FirstOrDefault(p => p.Key == gameElement.UnitAction);
            if (activeFactionUnits == null)
            {
                activeFactionUnits = new GroupedGameElement(gameElement.UnitAction, new List<IDisplayMatchedListView>());
                GetActiveFaction().Add(activeFactionUnits);
            }
            
            activeFactionUnits.Add(gameElement);
            gameElement.Invalidated += OnGroupedElement_Invalidated;

            RefreshObservbableCollection(ActiveFaction);
            SelectedActiveUnit = gameElement;
        }

        private ObservableCollection<GroupedGameElement> GetActiveFaction() => PlayerOneFactionVisible ? PlayerOneUnits : PlayerTwoUnits;
        
        private void SetActiveFlagOnSelectedElement()
        {
            // while the SelectedItem binding works fine, the visuals in UWP do not.  This flag was set on the base model to change text
            // colors around when items are selected.
            foreach(var group in GetActiveFaction())
            foreach (var element in group)
            {
                element.IsSelected = false;
            }

            if (SelectedActiveUnit == null) return;
            SelectedActiveUnit.IsSelected = true;
        }

        private void SetDefaultPanelVisible()
        {
            if (_selectedActiveUnit != null)
            {
                SetAllPanelsInvisible();
                MatchTrackingViewVisible = true;
            }
        }

        /// <summary>
        /// Takes the factionID of the observable collection and turns it into a single List of <see cref="IDisplayMatchedListView"/> elements.
        /// </summary>
        /// <returns></returns>
        private List<IDisplayMatchedListView> FlattenFactionUnitsToList(int factionID)
        {
            var flattenedList = new List<IDisplayMatchedListView>();
            foreach (var groupedElement in _factionUnits[factionID])
            {
                flattenedList.AddRange(groupedElement.GameElements);
            }

            return flattenedList;
        }

        private ObservableCollection<GroupedGameElement> GetGroupedFactionDataFromSavedMatchState(IEnumerable<IDisplayMatchedListView> data)
        {
            // I know there is likely a linq way to group by and do this, but for time sake I just decided to go the brute force direction
            // the idea here is taking one of the flat lists in cache and turning it into the grouped game element observable.
            var activeUnits = new ObservableCollection<GroupedGameElement>();
            var readyUnits = GroupElementsByAction(ActionsFactory.NO_ACTION, data);
            var movedUnits = GroupElementsByAction(ActionsFactory.MOVED, data);
            var shotUnits = GroupElementsByAction(ActionsFactory.WEAPONS_SHOT, data);
            var meleeUnits = GroupElementsByAction(ActionsFactory.MELEE, data);

            if (readyUnits.Any()) activeUnits.Add(readyUnits);
            if (movedUnits.Any()) activeUnits.Add(movedUnits);
            if (shotUnits.Any()) activeUnits.Add(shotUnits);
            if (meleeUnits.Any()) activeUnits.Add(meleeUnits);

            return activeUnits;
        }

        private void RefreshObservbableCollection(int factionID)
        {
            var data = _factionUnits[factionID];

            // This goes through the entire observable collection and reorganizes it.
            var readyUnits = new List<IDisplayMatchedListView>();
            var movedUnits = new List<IDisplayMatchedListView>();
            var shotUnits = new List<IDisplayMatchedListView>();
            var meleeUnits = new List<IDisplayMatchedListView>();

            foreach (var groupedCollection in data)
            {
                foreach (var element in groupedCollection)
                {
                    switch (element.UnitAction)
                    {
                        case ActionsFactory.NO_ACTION:
                            readyUnits.Add(element);
                            break;
                        case ActionsFactory.MOVED:
                            movedUnits.Add(element);
                            break;
                        case ActionsFactory.WEAPONS_SHOT:
                            shotUnits.Add(element);
                            break;
                        case ActionsFactory.MELEE:
                            meleeUnits.Add(element);
                            break;
                        default:
                            throw new NotImplementedException(
                                $"Unit action {element.UnitAction} has not been implemented");
                    }
                }
            }

            data.Clear();
            if (readyUnits.Any()) data.Add(new GroupedGameElement(ActionsFactory.NO_ACTION, 
                readyUnits.OrderBy(p=>p.UnitHeader)));
            if (movedUnits.Any()) data.Add(new GroupedGameElement(ActionsFactory.MOVED,
                movedUnits.OrderBy(p => p.UnitHeader)));
            if (shotUnits.Any()) data.Add(new GroupedGameElement(ActionsFactory.WEAPONS_SHOT,
                shotUnits.OrderBy(p => p.UnitHeader)));
            if (meleeUnits.Any()) data.Add(new GroupedGameElement(ActionsFactory.MELEE,
                meleeUnits.OrderBy(p => p.UnitHeader)));
        }

        private GroupedGameElement GroupElementsByAction(string action, IEnumerable<IDisplayMatchedListView> data)
        {
            var filteredData = data.Where(p => p.UnitAction == action)
                .OrderBy(p => p.UnitHeader);
            return new GroupedGameElement(action, filteredData);
        }

        private void RemoveUnitFromCollectionByID(Guid id)
        {
            foreach (var gameElement in GetActiveFaction())
            {
                var unit = gameElement.GameElements.FirstOrDefault(x => x.ID == id);
                if (unit == null) continue;

                gameElement.GameElements.Remove(unit);
            }
        }

        private void OnGroupedElement_Invalidated(object sender, EventArgs e)
        {
            //this will fire off everytime a unit action in one of the groupings changes, 
            //causing the entire observable to need to be redone.

            if (!_batchUpdateActive)
                RefreshObservbableCollection(ActiveFaction);
        }

        /// <summary>
        /// Goes through the non active faction elements and re-figures its target modifiers
        /// </summary>
        private void GetUpdatedTargetModifiers()
        {
            if (SelectedActiveUnit == null) return;
            var nonActiveFaction = ActiveFaction == 1 ? 0 : 1;
            SelectedActiveUnit.RefreshComponentStatus();

            var targetables = new List<ITargetable>();
            
            foreach (var element in _factionUnits[nonActiveFaction])
            {
                targetables.AddRange(element.GameElements.Cast<ITargetable>());
            }

            SelectedActiveUnit.ValidTargets.Targets = new ObservableCollection<TargetedEntity>(
                TargetingSystem.GetAllTargetedElementsModifiers(targetables, SelectedActiveUnit));
        }
    }
}
