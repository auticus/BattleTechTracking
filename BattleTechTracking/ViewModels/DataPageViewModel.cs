using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Factories;
using BattleTechTracking.Models;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    internal class DataPageViewModel : BaseViewModel
    {
        private IVehicleDetailView _selectedUnit;
        private string _unitNameFilter;
        private string _selectedUnitFilter;
        private bool _vehicleComponentsVisible;
        private bool _equipmentVisible;
        private bool _weaponsVisible;
        private bool _damageCodesVisible;
        private bool _ammoVisible;
        private bool _quirksVisible;
        private string _ammunitionViewHeader;
        private string _walkingLabel;
        private string _runningLabel;
        private string _jumpingLabel;

        private ObservableCollection<IDisplayListView> _visibleUnits;
        private ObservableCollection<UnitComponent> _selectedUnitComponents;
        private ObservableCollection<Equipment> _selectedUnitEquipment;
        private ObservableCollection<Weapon> _selectedUnitWeapons;
        private ObservableCollection<Weapon> _templatedWeapons;
        private ObservableCollection<Ammunition> _selectedWeaponAmmunition;
        private ObservableCollection<Quirk> _selectedUnitQuirks;

        private UnitComponent _selectedComponent;
        private Equipment _selectedEquipment;
        private Weapon _selectedWeapon;
        private Weapon _selectedTemplatedWeapon;
        private Ammunition _selectedAmmo;
        private List<BattleMech> _mechList;
        private List<IndustrialMech> _industrialMechList;
        private List<Infantry> _infantryList;
        private List<CombatVehicle> _combatVehicleList;
        private string _damageCodesCommaSeparated;
        private string _selectedQuirk;
        
        public ObservableCollection<string> UnitFilters { get; }

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

        /// <summary>
        /// Gets or sets the currently selected unit.
        /// </summary>
        public IVehicleDetailView SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                PersistSelectedUnitToModels();
                if (_selectedUnit != null) UpdateTemplatedWeaponsCacheWithAnyNewWeapons(_selectedUnit.Weapons);
                
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));

                SetObservableCollectionsFromSelectedModel();
                VehicleComponentsVisible = true;
            }
        }

        /// <summary>
        /// Gets or sets the unit components for the selected unit.
        /// </summary>
        public ObservableCollection<UnitComponent> SelectedUnitComponents
        {
            get => _selectedUnitComponents;
            private set
            {
                _selectedUnitComponents = value;
                OnPropertyChanged(nameof(SelectedUnitComponents));
            }
        }

        /// <summary>
        /// Gets or sets the equipment for the selected unit.
        /// </summary>
        public ObservableCollection<Equipment> SelectedUnitEquipment
        {
            get => _selectedUnitEquipment;
            private set
            {
                _selectedUnitEquipment = value;
                OnPropertyChanged(nameof(SelectedUnitEquipment));
            }
        }

        /// <summary>
        /// Gets or sets the selected unit weapons.
        /// </summary>
        public ObservableCollection<Weapon> SelectedUnitWeapons
        {
            get => _selectedUnitWeapons;
            private set
            {
                _selectedUnitWeapons = value;
                OnPropertyChanged(nameof(SelectedUnitWeapons));
            }
        }

        /// <summary>
        /// Gets the collection of weapons that are stored in the weapons template for creating new templates.
        /// </summary>
        public ObservableCollection<Weapon> TemplatedWeapons
        {
            get => _templatedWeapons;
            private set
            {
                _templatedWeapons = value;
                OnPropertyChanged(nameof(TemplatedWeapons));
            }
        }

        public ObservableCollection<Quirk> SelectedUnitQuirks
        {
            get => _selectedUnitQuirks;
            private set
            {
                _selectedUnitQuirks = value;
                OnPropertyChanged(nameof(SelectedUnitQuirks));
            }
        }

        /// <summary>
        /// Gets or sets the selected weapon's ammunition.
        /// </summary>
        public ObservableCollection<Ammunition> SelectedWeaponAmmunition
        {
            get => _selectedWeaponAmmunition;
            private set
            {
                _selectedWeaponAmmunition = value;
                OnPropertyChanged(nameof(SelectedWeaponAmmunition));
            }
        }

        /// <summary>
        /// Gets or sets the selected component.
        /// </summary>
        public UnitComponent SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                OnPropertyChanged(nameof(SelectedComponent));
            }
        }

        /// <summary>
        /// Gets or sets the selected equipment.
        /// </summary>
        public Equipment SelectedEquipment
        {
            get => _selectedEquipment;
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged(nameof(SelectedEquipment));
            }
        }

        /// <summary>
        /// Gets or sets the selected weapon.
        /// </summary>
        public Weapon SelectedWeapon
        {
            get => _selectedWeapon;
            set
            {
                _selectedWeapon = value;
                OnPropertyChanged(nameof(SelectedWeapon));
                AmmunitionViewHeader = $"{_selectedWeapon.Name ?? string.Empty} Ammunition";
                SelectedWeaponAmmunition = new ObservableCollection<Ammunition>(SelectedWeapon.Ammo);
            }
        }

        /// <summary>
        /// Gets or sets the selected Templated Weapon
        /// </summary>
        public Weapon SelectedTemplatedWeapon
        {
            get => _selectedTemplatedWeapon;
            set
            {
                _selectedTemplatedWeapon = value;
                OnPropertyChanged(nameof(SelectedTemplatedWeapon));
            }
        }

        public Ammunition SelectedAmmunition
        {
            get => _selectedAmmo;
            set
            {
                _selectedAmmo = value;
                OnPropertyChanged(nameof(SelectedAmmunition));
            }
        }

        public string SelectedQuirk
        {
            get => _selectedQuirk;
            set
            {
                _selectedQuirk = value;
                OnPropertyChanged(nameof(SelectedQuirk));
            }
        }

        public string UnitNameFilter
        {
            get => _unitNameFilter;
            set
            {
                if (_unitNameFilter == value) return;
                _unitNameFilter = value;
                OnPropertyChanged(nameof(UnitNameFilter));
            }
        }

        public string AmmunitionViewHeader
        {
            get => _ammunitionViewHeader;
            set
            {
                if (_ammunitionViewHeader == value) return;
                _ammunitionViewHeader = value;
                OnPropertyChanged(nameof(AmmunitionViewHeader));
            }
        }

        /// <summary>
        /// Gets or sets the value determining if the Vehicle location components panel is visible.
        /// </summary>
        public bool VehicleComponentsVisible
        {
            get => _vehicleComponentsVisible;
            set
            {
                if (_vehicleComponentsVisible == value) return;
                _vehicleComponentsVisible = value;
                OnPropertyChanged(nameof(VehicleComponentsVisible));
                if (_vehicleComponentsVisible) HideAllPanelsExceptForItemPassed(nameof(VehicleComponentsVisible));
            }
        }

        public bool EquipmentVisible
        {
            get => _equipmentVisible;
            set
            {
                if (_equipmentVisible == value) return;
                _equipmentVisible = value;
                OnPropertyChanged(nameof(EquipmentVisible));
                if (_equipmentVisible) HideAllPanelsExceptForItemPassed(nameof(EquipmentVisible));
            }
        }

        public bool WeaponsVisible
        {
            get => _weaponsVisible;
            set
            {
                if (_weaponsVisible == value) return;
                _weaponsVisible = value;
                OnPropertyChanged(nameof(WeaponsVisible));
                if (_weaponsVisible) HideAllPanelsExceptForItemPassed(nameof(WeaponsVisible));
            }
        }

        public bool DamageCodesVisible
        {
            get => _damageCodesVisible;
            set
            {
                if (_damageCodesVisible == value) return;
                _damageCodesVisible = value;
                OnPropertyChanged(nameof(DamageCodesVisible));
                if (_damageCodesVisible) HideAllPanelsExceptForItemPassed(nameof(DamageCodesVisible));
            }
        }

        public bool AmmoVisible
        {
            get => _ammoVisible;
            set
            {
                if (_ammoVisible == value) return;
                _ammoVisible = value;
                OnPropertyChanged(nameof(AmmoVisible));
                if(_ammoVisible) HideAllPanelsExceptForItemPassed(nameof(AmmoVisible));
            }
        }

        public bool QuirksVisible
        {
            get => _quirksVisible;
            set
            {
                if (_quirksVisible == value) return;
                _quirksVisible = value;
                OnPropertyChanged(nameof(QuirksVisible));
                if (_quirksVisible) HideAllPanelsExceptForItemPassed(nameof(QuirksVisible));
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

                if (_selectedUnitFilter != null)
                {
                    PersistSelectedUnitToList();
                }
                
                _selectedUnitFilter = value;
                OnPropertyChanged(nameof(SelectedUnitFilter));
                LoadVisibleUnits();
            }
        }

        /// <summary>
        /// Gets or sets the damage codes in the Damage Code view in a comma separated format.
        /// </summary>
        public string DamageCodesCommaSeparated
        {
            get => _damageCodesCommaSeparated;
            set
            {
                _damageCodesCommaSeparated = value;
                OnPropertyChanged(nameof(DamageCodesCommaSeparated));
            }
        }

        public string WalkingLabel
        {
            get => _walkingLabel;
            set
            {
                _walkingLabel = value;
                OnPropertyChanged(nameof(WalkingLabel));
            }
        }

        public string RunningLabel
        {
            get => _runningLabel;
            set
            {
                _runningLabel = value;
                OnPropertyChanged(nameof(RunningLabel));
            }
        }

        public string JumpingLabel
        {
            get => _jumpingLabel;
            set
            {
                _jumpingLabel = value;
                OnPropertyChanged(nameof(JumpingLabel));
            }
        }

        public ICommand NewComponent { get; }
        public ICommand DeleteComponent { get; }
        public ICommand NewEquipment { get; }
        public ICommand DeleteEquipment { get; }
        public ICommand CopyEquipment { get; }
        public ICommand NewWeapon { get; }
        public ICommand DeleteWeapon { get; }
        public ICommand CopyWeapon { get; }
        public ICommand OpenDamageCodes { get; }
        public ICommand OpenAmmo { get; }
        public ICommand NewAmmo { get; }
        public ICommand DeleteAmmo { get; }
        public ICommand FilterUnits { get; }
        
        /// <summary>
        /// Gets the command responsible for showing the Unit Components panel.
        /// </summary>
        public ICommand UnitComponentCommand { get; }
        
        /// <summary>
        /// Gets the command responsible for showing the Equipment panel.
        /// </summary>
        public ICommand EquipmentCommand { get; }
        
        /// <summary>
        /// Gets the command responsible for showing the Weapons panel.
        /// </summary>
        public ICommand WeaponsCommand { get; }

        /// <summary>
        /// Gets the command responsible for showing the quirks panel.
        /// </summary>
        public ICommand QuirksCommand { get; }

        /// <summary>
        /// Gets the command that runs when the Ok button on the Damage Codes view is pressed.
        /// </summary>
        public ICommand DamageCodesOkCommand { get; }

        /// <summary>
        /// Gets the command that runs when the Ok button on the Ammunition view is pressed.
        /// </summary>
        public ICommand AmmunitionOkCommand { get; }

        /// <summary>
        /// Gets the command that will create a new unit.
        /// </summary>
        public ICommand NewUnit { get; }

        /// <summary>
        /// Gets the command that will delete a unit.
        /// </summary>
        public ICommand DeleteUnit { get; }

        /// <summary>
        /// Gets the command that will save all changes and return to the main page.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Gets the command that will close the data view out and return to the main page.
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Gets the command that will create a new quirk.
        /// </summary>
        public ICommand NewQuirk { get; }

        /// <summary>
        /// Gets the command that will delete a quirk.
        /// </summary>
        public ICommand DeleteQuirk { get; }

        public DataPageViewModel()
        {
            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            VisibleUnits = new ObservableCollection<IDisplayListView>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();
            _industrialMechList = DataPump.GetPersistedDataForType<IndustrialMech>().ToList();
            _infantryList = DataPump.GetPersistedDataForType<Infantry>().ToList();
            _combatVehicleList = DataPump.GetPersistedDataForType<CombatVehicle>().ToList();

            TemplatedWeapons = new ObservableCollection<Weapon>(
                ComponentFactory.GetDistinctWeapons(new[]
                { 
                    _mechList.ConvertAll(x=>(BaseUnit)x), 
                    _industrialMechList.ConvertAll(x=>(BaseUnit)x), 
                    _combatVehicleList.ConvertAll(x=>(BaseUnit)x)
                }, UnitComponent.CENTER_TORSO_CODE));

            SelectedTemplatedWeapon = TemplatedWeapons[0];

            SelectedUnitFilter = UnitTypes.BATTLE_MECH;
            VehicleComponentsVisible = true;

            NewComponent = new Command(() =>
            {
                if (SelectedUnit == null) return;
                SelectedUnitComponents.Add(new UnitComponent(){Name = "Unnamed"});
            });

            DeleteComponent = new Command<Guid>((id) =>
            {
                var item = SelectedUnitComponents.FirstOrDefault(x => x.ID == id);
                if (item == null) return;
                SelectedUnitComponents.Remove(item);
            });

            NewEquipment = new Command(() =>
            {
                if (SelectedUnit == null) return;
                SelectedUnitEquipment.Add(new Equipment() { Name = "Unnamed", Hits = 1, Location="CT" });
            });

            DeleteEquipment = new Command<Guid>((id) =>
            {
                var item = SelectedUnitEquipment.FirstOrDefault(x => x.ID == id);
                if (item == null) return;
                SelectedUnitEquipment.Remove(item);
            });

            CopyEquipment = new Command<Guid>((id) =>
            {
                var item = SelectedUnitEquipment.FirstOrDefault(x => x.ID == id);
                if (item == null) return;
                SelectedUnitEquipment.Add(item.Copy());
            });

            NewWeapon = new Command(() =>
            {
                if (SelectedUnit == null) return;

                //intentionally do not carry ammunition locations over
                var newWpn = ComponentFactory.BuildWeaponFromTemplate(
                    SelectedTemplatedWeapon, 
                    overrideLocation: UnitComponent.DefaultLocation((BaseUnit)SelectedUnit), 
                    keepTemplatedAmmo: false);

                SelectedUnitWeapons.Add(newWpn);
            });

            DeleteWeapon = new Command<Guid>((id) =>
            {
                var weapon = SelectedUnitWeapons.FirstOrDefault(x => x.ID == id);
                if (weapon == null) return;
                SelectedUnitWeapons.Remove(weapon);
            });

            CopyWeapon = new Command<Guid>((id) =>
            {
                var weapon = SelectedUnitWeapons.FirstOrDefault(x => x.ID == id);
                if (weapon == null) return;
                SelectedUnitWeapons.Add(ComponentFactory.BuildWeaponFromTemplate(weapon));
            });

            UnitComponentCommand = new Command(() =>
            {
                VehicleComponentsVisible = true;
            });

            EquipmentCommand = new Command(() =>
            {
                EquipmentVisible = true;
            });

            WeaponsCommand = new Command(() =>
            {
                WeaponsVisible = true;
            });

            QuirksCommand = new Command(() =>
            {
                QuirksVisible = true;
            });

            OpenDamageCodes = new Command<Guid>((id) =>
            {
                var weapon = SelectedUnitWeapons.FirstOrDefault(x => x.ID == id);
                if (weapon == null) return;

                //SelectedWeapon may not be selected if the user just clicked the button (it doesn't select the entity)
                SelectedWeapon = weapon;

                DamageCodesCommaSeparated = string.Join(",", weapon.DamageCodes.ToArray());
                DamageCodesVisible = true;
            });

            DamageCodesOkCommand = new Command(() =>
            {
                //the selected weapon property should have been set in OpenCommandCodes
                SelectedWeapon.DamageCodes = DamageCodesCommaSeparated.Split(',')
                    .Select(x=>x.Trim()).ToList();
                WeaponsVisible = true;
            });

            OpenAmmo = new Command<Guid>((id) =>
            {
                var weapon = SelectedUnitWeapons.FirstOrDefault(x => x.ID == id);
                if (weapon == null) return;

                //SelectedWeapon may not be selected if the user just clicked the button (it doesn't select the entity)
                SelectedWeapon = weapon;
                AmmoVisible = true;
            });

            NewAmmo = new Command(() =>
            {
                if (SelectedWeaponAmmunition == null) return;
                SelectedWeaponAmmunition.Add(new Ammunition(){Name=$"Ammo ({SelectedWeapon.Name})", 
                    Hits = 1, 
                    Location="CT",
                    AmmoCount = 10,
                    WeaponDamage = ComponentFactory.GetAmmunitionDamagePerShotFromName(SelectedWeapon)});
            });

            DeleteAmmo = new Command<Guid>((id) =>
            {
                var ammo = SelectedWeaponAmmunition.FirstOrDefault(x => x.ID == id);
                if (ammo == null) return;
                SelectedWeaponAmmunition.Remove(ammo);
            });

            AmmunitionOkCommand = new Command(() =>
            {
                //the selected weapon property should have been set in OpenAmmo
                SelectedWeapon.Ammo = SelectedWeaponAmmunition.ToList();
                WeaponsVisible = true;
            });

            NewUnit = new Command(() =>
            {
                VisibleUnits.Add(UnitTypes.BuildNewUnitFromType(SelectedUnitFilter));
            });

            DeleteUnit = new Command<Guid>((id) =>
            {
                var unit = VisibleUnits.FirstOrDefault(x => x.ID == id);
                if (unit == null) return;
                VisibleUnits.Remove(unit);
            });

            OkCommand = new Command(() =>
            {
                // finish out saving whatever is currently loaded
                PersistSelectedUnitToList();

                // now go ahead and save all of the files.
                DataPump.SavePersistedDataForType(_mechList);
                DataPump.SavePersistedDataForType(_industrialMechList);
                DataPump.SavePersistedDataForType(_infantryList);
                DataPump.SavePersistedDataForType(_combatVehicleList);
                PageNavigation.PopAsync();
            });

            CloseCommand = new Command(() =>
            {
                PageNavigation.PopAsync();
            });

            NewQuirk = new Command(() =>
            {
                if (SelectedUnitQuirks == null) return;
                SelectedUnitQuirks.Add(new Quirk(){Name="Unknown"});
            });

            DeleteQuirk = new Command<Quirk>((quirk) =>
            {
                SelectedUnitQuirks.Remove(quirk);
            });

            FilterUnits = new Command(FilterVisibleUnitsBySearchCondition);
        }

        private void LoadVisibleUnits()
        {
            VisibleUnits = new ObservableCollection<IDisplayListView>(GetAssociatedUnitsByFilterType());
            SetAssociatedViewLabelsByFilterType();
        }

        private void FilterVisibleUnitsBySearchCondition()
        {
            //fires off whenever the user clicks the magnifying glass icon to filter down whatever is currently loaded
            LoadVisibleUnits();
            if (string.IsNullOrEmpty(UnitNameFilter)) return;
            
            var filteredList = VisibleUnits.Where(unit => unit.UnitHeader.Contains(UnitNameFilter)).ToList();
            VisibleUnits = new ObservableCollection<IDisplayListView>(filteredList);
        }

        private IEnumerable<IDisplayListView> GetAssociatedUnitsByFilterType()
        {
            switch (SelectedUnitFilter)
            {
                case UnitTypes.BATTLE_MECH:
                    return _mechList.OrderBy(p=>p.Name).ThenBy(p=>p.Model);
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

        private void SetAssociatedViewLabelsByFilterType()
        {
            switch (SelectedUnitFilter)
            {
                case UnitTypes.BATTLE_MECH:
                case UnitTypes.INDUSTRIAL_MECH:
                    SetMechMovementLabels();
                    break;
                case UnitTypes.COMBAT_VEHICLE:
                case UnitTypes.SUPPORT_VEHICLE:
                    SetVehiclesMovementLabels();
                    break;
            }
        }

        private void PersistSelectedUnitToList()
        {
            PersistSelectedUnitToModels();

            switch (SelectedUnitFilter)
            {
                case UnitTypes.BATTLE_MECH:
                    _mechList = VisibleUnits.Cast<BattleMech>().ToList();
                    break;
                case UnitTypes.INDUSTRIAL_MECH:
                    _industrialMechList = VisibleUnits.Cast<IndustrialMech>().ToList();
                    break;
                case UnitTypes.COMBAT_VEHICLE:
                    _combatVehicleList = VisibleUnits.Cast<CombatVehicle>().ToList();
                    break;
                //infantry is not a display unit
                default:
                    throw new NotImplementedException($"The selected unit type {SelectedUnitFilter} does not exist");
            }
        }

        private void PersistSelectedUnitToModels()
        {
            if (_selectedUnit == null) return;
            _selectedUnit.Components = SelectedUnitComponents.ToList();
            _selectedUnit.Equipment = SelectedUnitEquipment.ToList();
            _selectedUnit.Weapons = SelectedUnitWeapons.ToList();
            _selectedUnit.Quirks = SelectedUnitQuirks.ToList();
        }

        private void SetObservableCollectionsFromSelectedModel()
        {
            if (_selectedUnit == null)
            {
                SelectedUnitComponents?.Clear();
                SelectedUnitEquipment?.Clear();
                SelectedUnitWeapons?.Clear();
                SelectedUnitQuirks?.Clear();
                return;
            }

            SelectedUnitComponents = new ObservableCollection<UnitComponent>(_selectedUnit.Components);
            SelectedUnitEquipment = new ObservableCollection<Equipment>(_selectedUnit.Equipment.OrderBy(p => p.Location).ThenBy(p=>p.Name));
            SelectedUnitWeapons = new ObservableCollection<Weapon>(_selectedUnit.Weapons);
            SelectedUnitQuirks = new ObservableCollection<Quirk>(_selectedUnit.Quirks);
        }

        private void HideAllSubPanels()
        {
            HideAllPanelsExceptForItemPassed(string.Empty);
        }

        private void HideAllPanelsExceptForItemPassed(string panelToKeepVisible)
        {
            if (panelToKeepVisible != nameof(VehicleComponentsVisible)) VehicleComponentsVisible = false;
            if (panelToKeepVisible != nameof(EquipmentVisible)) EquipmentVisible = false;
            if (panelToKeepVisible != nameof(WeaponsVisible)) WeaponsVisible = false;
            if (panelToKeepVisible != nameof(DamageCodesVisible)) DamageCodesVisible = false;
            if (panelToKeepVisible != nameof(AmmoVisible)) AmmoVisible = false;
            if (panelToKeepVisible != nameof(QuirksVisible)) QuirksVisible = false;
        }

        private void SetMechMovementLabels()
        {
            WalkingLabel = "Walking";
            RunningLabel = "Running";
            JumpingLabel = "Jumping";
        }

        private void SetVehiclesMovementLabels()
        {
            WalkingLabel = "Cruising";
            RunningLabel = "Flanking";
            JumpingLabel = "Flying";
        }

        private void UpdateTemplatedWeaponsCacheWithAnyNewWeapons(IEnumerable<Weapon> weapons)
        {
            // examine the weapons collection passed in and add any items that need to be added that do not yet exist
            foreach (var wpn in weapons)
            {
                if (TemplatedWeapons.Any(p => p.Name.Trim() == wpn.Name.Trim())) continue;
                TemplatedWeapons.Add(wpn);
            }
        }
    }
}
