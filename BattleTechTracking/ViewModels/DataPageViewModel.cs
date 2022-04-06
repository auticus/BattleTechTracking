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
    internal class DataPageViewModel : BaseViewModel
    {
        private IDisplayUnit _selectedUnit;
        private string _unitNameFilter;
        private string _selectedUnitFilter;
        private bool _vehicleComponentsVisible;
        private bool _equipmentVisible;
        private bool _weaponsVisible;
        private ObservableCollection<IDisplayUnit> _visibleUnits;
        private ObservableCollection<UnitComponent> _selectedUnitComponents;
        private ObservableCollection<Equipment> _selectedUnitEquipment;
        private ObservableCollection<Weapon> _selectedUnitWeapons;
        private UnitComponent _selectedComponent;
        private Equipment _selectedEquipment;
        private readonly List<BattleMech> _mechList;
        
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

        public ICommand NewComponent { get; }
        public ICommand DeleteComponent { get; }
        public ICommand NewEquipment { get; }
        public ICommand DeleteEquipment { get; }
        
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
