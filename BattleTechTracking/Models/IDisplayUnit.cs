namespace BattleTechTracking.Models
{
    /// <summary>
    /// Interface for elements that will be displayed in the Data View list element.
    /// </summary>
    public interface IDisplayUnit
    {
        /// <summary>
        /// The given name of the unit.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The nomenclature of the unit.
        /// </summary>
        string Model { get; set; }

        int Tonnage { get; set; }

        int BattleValue { get; set; }

        string TechBase { get; set; }
        string RulesLevel { get; set; }

        int YearIntroduced { get; set; }
        int? YearExtinct { get; set; }
        int HeatSinks { get; set; }

        Movement UnitMovement { get; set; }
        string UnitHeader { get; }
        string UnitDetails { get; }
    }
}
