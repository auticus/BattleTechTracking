using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Reports
{
    /// <summary>
    /// Interface used for elements that can have Reports run on them.
    /// </summary>
    public interface IReportable
    {
        string UnitHeader { get; }
        IEnumerable<UnitComponent> UnitComponents { get; }
        IEnumerable<Equipment> UnitEquipment { get; }
        IEnumerable<Weapon> UnitWeapons { get; }
        IEnumerable<Ammunition> UnitAmmunition { get; }
    }
}
