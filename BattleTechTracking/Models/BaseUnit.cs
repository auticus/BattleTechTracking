using System.Collections.Generic;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// The base model of all units in the application.
    /// </summary>
    public abstract class BaseUnit : IDisplayUnit
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Model { get; set; }

        public int Tonnage { get; set; }

        public int BattleValue { get; set; }
        public string TechBase { get; set; }
        public string RulesLevel { get; set; }

        public int YearIntroduced { get; set; }
        public int? YearExtinct { get; set; }

        public Movement UnitMovement { get; set; }

        public IEnumerable<UnitComponent> Components { get; set; } = new List<UnitComponent>();

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
