using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BattleTechTracking.Factories;
using BattleTechTracking.Models;

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
        private UnitComponent _selectedComponent;
        private ObservableCollection<IDisplayUnit> _visibleUnits;
        private ObservableCollection<UnitComponent> _selectedUnitComponents;
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

        public IDisplayUnit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));

                if (_selectedUnit == null) return;
                SelectedUnitComponents = new ObservableCollection<UnitComponent>(_selectedUnit.Components);
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

        public bool VehicleComponentsVisible
        {
            get => _vehicleComponentsVisible;
            set
            {
                if (_vehicleComponentsVisible == value) return;
                _vehicleComponentsVisible = value;
                OnPropertyChanged(nameof(VehicleComponentsVisible));
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

        public DataPageViewModel()
        {
            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            VisibleUnits = new ObservableCollection<IDisplayUnit>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();

            SelectedUnitFilter = UnitTypes.BATTLE_MECH;
            VehicleComponentsVisible = true;
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
