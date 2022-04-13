using System.Collections.Generic;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// The base model of all units in the application.
    /// </summary>
    public abstract class BaseUnit : BaseModel, IVehicleDetailView, IDisplayListView
    {
        private string _name;
        private string _model;
        private int _tonnage;
        private int _battleValue;

        /// <inheritdoc/>
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(UnitHeader));
            }
        }

        /// <inheritdoc/>
        public string Model
        {
            get => _model;
            set
            {
                if (_model == value) return;
                _model = value;
                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(UnitHeader));
            }
        }

        public int Tonnage
        {
            get => _tonnage;
            set
            {
                if (_tonnage == value) return;
                _tonnage = value;
                OnPropertyChanged(nameof(Tonnage));
                OnPropertyChanged(nameof(UnitDetails));
            }
        }

        public int BattleValue
        {
            get => _battleValue;
            set
            {
                if (_battleValue == value) return;
                _battleValue = value;
                OnPropertyChanged(nameof(BattleValue));
                OnPropertyChanged(nameof(UnitDetails));
            }
        }

        public string TechBase { get; set; }
        public string RulesLevel { get; set; }

        public int YearIntroduced { get; set; }
        public int? YearExtinct { get; set; }

        public Movement UnitMovement { get; set; } = new Movement();

        public IEnumerable<UnitComponent> Components { get; set; } = new List<UnitComponent>();
        
        // todo: if you want to add details to each piece of gear - don't do it on each mech - need a master list of gear and their details
        public IEnumerable<Equipment> Equipment { get; set; } = new List<Equipment>();
        public IEnumerable<Weapon> Weapons { get; set; } = new List<Weapon>();

        /// <summary>
        /// The amount of heat reduced by heat sinks.
        /// </summary>
        public int HeatSinks { get; set; }

        public IEnumerable<Quirk> Quirks { get; set; } = new List<Quirk>();

        public string UnitHeader => $"{Name} ({Model})";
        public string UnitDetails => $"{Tonnage} tons - BV: {BattleValue}";
    }
}
