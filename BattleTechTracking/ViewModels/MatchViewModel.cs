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

        private List<IDisplayMatchedListView>[] _factionUnits =
            { new List<IDisplayMatchedListView>(), new List<IDisplayMatchedListView>() };

        private ObservableCollection<GroupedGameElement> _activeFactionUnits;
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
                if (ActiveFactionUnits != null) _factionUnits[ActiveFaction] = FlattenActiveFactionUnitsToOneList();

                _activeFaction = value;
                OnPropertyChanged(nameof(ActiveFaction));

                //make the new active observable collection based on the new active faction
                ActiveFactionUnits = GetGroupedFactionDataFromFactionData(_factionUnits[ActiveFaction]);
                ActiveFactionName = ActiveFaction == 0 ? Faction1Name : Faction2Name;

                // this is a turbo hack - but UWP for whatever reason gets really angry if there is only one element that gets set null
                // it no longer registers selections and the add button stops working
                SelectedActiveUnit = ActiveFactionUnits.Count == 1
                    ? (TrackedGameElement)ActiveFactionUnits[0].GameElements[0]
                    : null;
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
        public ObservableCollection<GroupedGameElement> ActiveFactionUnits
        {
            get => _activeFactionUnits;
            set
            {
                _activeFactionUnits = value;

                foreach (var element in _activeFactionUnits)
                {
                    element.Invalidated += OnGroupedElement_Invalidated;
                }
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
                SetActiveFlagOnSelectedElement();
                SetDefaultPanelVisible();
                PopulateDataStructuresWithActiveUnit();
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

        public List<string> UnitActions { get; }
        public List<string> UnitStatuses { get; }

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
            UnitActions = ActionsFactory.BuildActionsList().ToList();
            UnitStatuses = UnitStatusFactory.BuildStatusList().ToList();
            SelectorViewVisibleUnits = new ObservableCollection<IDisplayListView>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();
            _industrialMechList = DataPump.GetPersistedDataForType<IndustrialMech>().ToList();
            _infantryList = DataPump.GetPersistedDataForType<Infantry>().ToList();
            _combatVehicleList = DataPump.GetPersistedDataForType<CombatVehicle>().ToList();

            SelectedUnitFilter = UnitTypes.BATTLE_MECH;

            OkCommand = new Command(() =>
            {
                //make sure our active faction is saved out to the cache
                _factionUnits[ActiveFaction] = FlattenActiveFactionUnitsToOneList();

                var matchState = new MatchState()
                {
                    Faction1Name = this.Faction1Name,
                    Faction2Name = this.Faction2Name,
                    Factions = _factionUnits
                };
                DataPump.SaveMatchState(matchState, "SavedMatchState.json");
                PageNavigation.PopAsync();
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
                var nonActiveFaction = ActiveFaction == 1 ? 0 : 1;
                foreach (var element in _factionUnits[nonActiveFaction])
                {
                    ((TrackedGameElement)element).NextRound();
                }

                foreach (var group in ActiveFactionUnits)
                {
                    foreach (var element in group.GameElements)
                    {
                        ((TrackedGameElement)element).NextRound();
                    }
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

        public void LoadMatchState(MatchState state)
        {
            if (state == null) return;

            Faction1Name = state.Faction1Name;
            Faction2Name = state.Faction2Name;

            _factionUnits = state.Factions;
            ActiveFactionUnits = GetGroupedFactionDataFromFactionData(_factionUnits[0]);
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
            var activeFactionUnits = ActiveFactionUnits.FirstOrDefault(p => p.Key == gameElement.UnitAction);
            if (activeFactionUnits == null)
            {
                activeFactionUnits = new GroupedGameElement(gameElement.UnitAction, new List<IDisplayMatchedListView>());
                ActiveFactionUnits.Add(activeFactionUnits);
            }
            
            activeFactionUnits.Add(gameElement);

            ActiveFactionUnits = GetGroupedFactionDataFromFactionData(ActiveFactionUnits);
            SelectedActiveUnit = gameElement;
        }

        private void SetActiveFlagOnSelectedElement()
        {
            // while the SelectedItem binding works fine, the visuals in UWP do not.  This flag was set on the base model to change text
            // colors around when items are selected.
            foreach(var group in ActiveFactionUnits)
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

        private void PopulateDataStructuresWithActiveUnit()
        {
            if (SelectedActiveUnit == null)
            {
                ActiveUnitComponents = null;
                ActiveUnitEquipment = null;
                ActiveUnitWeapons = null;
                ActiveUnitAmmunition = null;
            }
            else
            {
                ActiveUnitComponents =
                    new ObservableCollection<UnitComponent>(SelectedActiveUnit.UnitComponents.ToList());
                ActiveUnitEquipment = new ObservableCollection<Equipment>(SelectedActiveUnit.UnitEquipment.ToList());
                ActiveUnitWeapons = new ObservableCollection<Weapon>(SelectedActiveUnit.UnitWeapons.ToList());
                ActiveUnitAmmunition = new ObservableCollection<Ammunition>(SelectedActiveUnit.UnitAmmunition.ToList());
            }
        }

        private List<IDisplayMatchedListView> FlattenActiveFactionUnitsToOneList()
        {
            if (ActiveFactionUnits == null) return null;
            var flattenedList = new List<IDisplayMatchedListView>();
            foreach (var groupedElement in ActiveFactionUnits)
            {
                flattenedList.AddRange(groupedElement.GameElements);
            }

            return flattenedList;
        }

        private ObservableCollection<GroupedGameElement> GetGroupedFactionDataFromFactionData(IEnumerable<IDisplayMatchedListView> data)
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

        private ObservableCollection<GroupedGameElement> GetGroupedFactionDataFromFactionData(ObservableCollection<GroupedGameElement> data)
        {
            // This goes through the entire observable collection and reorganizes it.
            var activeUnits = new ObservableCollection<GroupedGameElement>();
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

            if (readyUnits.Any()) activeUnits.Add(new GroupedGameElement(ActionsFactory.NO_ACTION, 
                readyUnits.OrderBy(p=>p.UnitHeader)));
            if (movedUnits.Any()) activeUnits.Add(new GroupedGameElement(ActionsFactory.MOVED,
                movedUnits.OrderBy(p => p.UnitHeader)));
            if (shotUnits.Any()) activeUnits.Add(new GroupedGameElement(ActionsFactory.WEAPONS_SHOT,
                shotUnits.OrderBy(p => p.UnitHeader)));
            if (meleeUnits.Any()) activeUnits.Add(new GroupedGameElement(ActionsFactory.MELEE,
                meleeUnits.OrderBy(p => p.UnitHeader)));

            return activeUnits;
        }

        private GroupedGameElement GroupElementsByAction(string action, IEnumerable<IDisplayMatchedListView> data)
        {
            var filteredData = data.Where(p => p.UnitAction == action)
                .OrderBy(p => p.UnitHeader);
            return new GroupedGameElement(action, filteredData);
        }

        private void RemoveUnitFromCollectionByID(Guid id)
        {
            foreach (var gameElement in ActiveFactionUnits)
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
            UnsubAllGroupInvalidationEvents();

            //rebuild the collection
            ActiveFactionUnits = GetGroupedFactionDataFromFactionData(ActiveFactionUnits);

            //Attempting to set the active unit to what was selected produces an infinite loop
            //have not figured that out yet as to why it calls itself over and over
        }

        private void UnsubAllGroupInvalidationEvents()
        {
            foreach (var element in ActiveFactionUnits)
            {
                element.Invalidated -= OnGroupedElement_Invalidated;
            }
        }
    }
}
