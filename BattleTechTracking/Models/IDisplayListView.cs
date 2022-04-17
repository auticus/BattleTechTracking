using System;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Interface applied to units that must be displayed in a ListView control.
    /// </summary>
    public interface IDisplayListView
    {
        Guid ID { get; }
        bool IsSelected { get; set; }
        string UnitHeader { get; }
        string UnitDetails { get; }
    }
}
