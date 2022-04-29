using System;

namespace BattleTechTracking.Models
{
    public interface IDisplayMatchedListView : IDisplayListView
    {
        string UnitAction { get; set; }
        string UnitStatus { get; set; }
        EventHandler Invalidated { get; set; }
        EventHandler OnGunneryChanged { get; set; }
    }
}
