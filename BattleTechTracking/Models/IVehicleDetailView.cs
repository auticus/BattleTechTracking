using System;
using System.Collections.Generic;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Interface for elements that will be displayed in the Data View list element for mechs and vehicles..
    /// </summary>
    public interface IVehicleDetailView
    {
        /// <summary>
        /// Gets the unique ID of the unit.
        /// </summary>
        Guid ID { get; }

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
        IEnumerable<UnitComponent> Components { get; set; }
        IEnumerable<Equipment> Equipment { get; set; }
        IEnumerable<Weapon> Weapons { get; set; }
        IEnumerable<Quirk> Quirks { get; set; }
        string UnitHeader { get; }
        string UnitDetails { get; }
    }
}
