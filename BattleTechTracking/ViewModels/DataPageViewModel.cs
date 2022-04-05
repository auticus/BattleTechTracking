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

        private readonly List<BattleMech> _mechList = new List<BattleMech>();

        public ObservableCollection<string> UnitFilters { get; }
        public ObservableCollection<IDisplayUnit> VisibleUnits { get; private set; }

        public IDisplayUnit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                OnProperyChanged(nameof(SelectedUnit));
            }
        }

        public string UnitNameFilter
        {
            get => _unitNameFilter;
            set
            {
                if (_unitNameFilter == value) return;
                _unitNameFilter = value;
                OnProperyChanged(nameof(UnitNameFilter));
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
                OnProperyChanged(nameof(SelectedUnitFilter));
                LoadListViewWithSelectedUnitType();
            }
        }

        public DataPageViewModel()
        {
            UnitFilters = UnitTypes.BuildUnitTypesCollection();
            VisibleUnits = new ObservableCollection<IDisplayUnit>();

            _mechList = DataPump.GetPersistedDataForType<BattleMech>().ToList();

            SelectedUnitFilter = UnitTypes.BATTLE_MECH;
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
