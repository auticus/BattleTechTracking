namespace BattleTechTracking.Models
{
    public class TrackedUnitComponent : BaseModel
    {
        private int _currentArmor;
        private int? _currentRear;
        private int _currentStructure;

        /// <summary>
        /// Gets the TemplatedComponent that makes up the undamaged data state.
        /// </summary>
        public UnitComponent TemplatedComponent { get; }

        public int CurrentArmor
        {
            get => _currentArmor;
            set
            {
                _currentArmor = value;
                OnPropertyChanged(nameof(CurrentArmor));
            }
        }

        public int? CurrentRear
        {
            get => _currentRear;
            set
            {
                _currentRear = value;
                OnPropertyChanged(nameof(CurrentRear));
            }
        }

        public int CurrentStructure
        {
            get => _currentStructure;
            set
            {
                _currentStructure = value;
                OnPropertyChanged(nameof(CurrentStructure));
            }
        }

        public TrackedUnitComponent(UnitComponent baseComponent)
        {
            TemplatedComponent = baseComponent;
            CurrentArmor = baseComponent.Armor;
            CurrentRear = baseComponent.RearArmor;
            CurrentStructure = baseComponent.Structure;
        }
    }
}
