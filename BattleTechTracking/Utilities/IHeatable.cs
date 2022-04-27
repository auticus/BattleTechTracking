using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Interface that supports elements that can have heat tracking applied to them.
    /// </summary>
    public interface IHeatable
    {
        IDisplayListView GameElement { get; }
        int CurrentHeatLevel { get; set; }
        int CurrentHeatSinks { get; set; }
        IEnumerable<Equipment> UnitEquipment { get; }
    }
}
