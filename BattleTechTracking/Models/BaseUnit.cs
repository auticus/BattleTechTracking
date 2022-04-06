using System.Collections.Generic;
using System.ComponentModel;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// The base model of all units in the application.
    /// </summary>
    public abstract class BaseUnit : BaseModel, IDisplayUnit
    {
        private IEnumerable<UnitComponent> _components;
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

        public Movement UnitMovement { get; set; }

        public IEnumerable<UnitComponent> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged(nameof(Components));
            }
        }

        // todo: if you want to add details to each piece of gear - don't do it on each mech - need a master list of gear and their details
        public IEnumerable<Equipment> Equipment { get; set; } = new List<Equipment>();
        public IEnumerable<Weapon> Weapons { get; set; } = new List<Weapon>();

        /// <summary>
        /// The current heat level of the mech in play.
        /// </summary>
        public int CurrentHeatLevel { get; set; }

        /// <summary>
        /// The amount of heat reduced by heat sinks.
        /// </summary>
        public int HeatSinks { get; set; }

        public IEnumerable<string> Quirks { get; set; } = new List<string>();

        public string UnitHeader => $"{Name} ({Model})";
        public string UnitDetails => $"{Tonnage} tons - BV: {BattleValue}";
    }
}
