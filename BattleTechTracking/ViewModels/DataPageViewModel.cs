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
    internal class DataPageViewModel : BaseViewModel
    {
        private IDisplayUnit _selectedUnit;
        private string _unitNameFilter;
        private string _selectedUnitFilter;
        private bool _vehicleComponentsVisible;
        private bool _equipmentVisible;
        private bool _weaponsVisible;
        private bool _damageCodesVisible;
        private bool _ammoVisible;
        private string _ammunitionViewHeader;

        private ObservableCollection<IDisplayUnit> _visibleUnits;
        private ObservableCollection<UnitComponent> _selectedUnitComponents;
        private ObservableCollection<Equipment> _selectedUnitEquipment;
        private ObservableCollection<Weapon> _selectedUnitWeapons;
        private ObservableCollection<Ammunition> _selectedWeaponAmmunition;
        private UnitComponent _selectedComponent;
        private Equipment _selectedEquipment;
        private Weapon _selectedWeapon;
        private Ammunition _selectedAmmo;
        private readonly List<BattleMech> _mechList;
        private string _damageCodesCommaSeparated;
        
        public ObservableCollection<string> UnitFilters { get; }

        public ObservableCollection<IDisplayUnit> VisibleUnits
        {
            get => _visibleUnits;
            private set
            {
                _visibleUnits = value;
                OnPropertyChanged(nameof(VisibleUnits));
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

        public Ammunition SelectedAmmunition
        {
            get => _selectedAmmo;
            set
            {
                _selectedAmmo = value;
                OnPropertyChanged(nameof(SelectedAmmunition));
            }
        }

        public IDisplayUnit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));

                if (_selectedUnit == null) return;
                SelectedUnitComponents = new ObservableCollection<UnitComponent>(_selectedUnit.Components);
                SelectedUnitEquipment = new ObservableCollection<Equipment>(_selectedUnit.Equipment.OrderBy(p=>p.Location));
                SelectedUnitWeapons = new ObservableCollection<Weapon>(_selectedUnit.Weapons);
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

                if (value == false) return;
                EquipmentVisible = false;
                WeaponsVisible = false;
                DamageCodesVisible = false;
                AmmoVisible = false;
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

                if (value == false) return;
                VehicleComponentsVisible = false;
                WeaponsVisible = false;
                DamageCodesVisible = false;
                AmmoVisible = false;
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

                if (value == false) return;
                VehicleComponentsVisible = false;
                EquipmentVisible = false;
                DamageCodesVisible = false;
                AmmoVisible = false;
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

                if (value == false) return;
                VehicleComponentsVisible = false;
                EquipmentVisible = false;
                WeaponsVisible = false;
                AmmoVisible = false;
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

                if (value == false) return;
                VehicleComponentsVisible = false;
                EquipmentVisible = false;
                WeaponsVisible = false;
                DamageCodesVisible = false;
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
                LoadListViewWithSelectedUnitType();
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

        public ICommand NewComponent { get; }
        public ICommand DeleteComponent { get; }
        public ICommand NewEquipment { get; }
        public ICommand DeleteEquipment { get; }
        public ICommand NewWeapon { get; }
        public ICommand DeleteWeapon { get; }
        public ICommand OpenDamageCodes { get; }
        public ICommand OpenAmmo { get; }
        public ICommand NewAmmo { get; }
        public ICommand DeleteAmmo { get; }
        
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
        /// Gets the command that runs when the Ok button on the Damage Codes view is pressed.
        /// </summary>
        public ICommand DamageCodesOkCommand { get; }

        /// <summary>
        /// Gets the command that runs when the Ok button on the Ammunition view is pressed.
        /// </summary>
        public ICommand AmmunitionOkCommand { get; }

        public DataPageViewModel()
        {
            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            VisibleUnits = new ObservableCollection<IDisplayUnit>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();

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

            NewWeapon = new Command(() =>
            {
                if (SelectedUnit == null) return;
                SelectedUnitWeapons.Add(new Weapon(){Name = "Unnamed", Hits=1, Location="CT"});
            });

            DeleteWeapon = new Command<Guid>((id) =>
            {
                var item = SelectedUnitWeapons.FirstOrDefault(x => x.ID == id);
                if (item == null) return;
                SelectedUnitWeapons.Remove(item);
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
                SelectedWeaponAmmunition.Add(new Ammunition(){Name=$"Ammo ({SelectedWeapon.Name})", Hits = 1, Location="CT", AmmoCount = 10});
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
        }

        private void LoadListViewWithSelectedUnitType()
        {
            VisibleUnits = new ObservableCollection<IDisplayUnit>(GetAssociatedUnitsByFilterType());
        }

        private IEnumerable<IDisplayUnit> GetAssociatedUnitsByFilterType()
        {
            switch (SelectedUnitFilter)
            {
                case UnitTypes.BATTLE_MECH:
                    return _mechList;
                default:
                    throw new NotImplementedException($"The selected unit type {SelectedUnitFilter} does not exist");
            }
        }
    }
}
