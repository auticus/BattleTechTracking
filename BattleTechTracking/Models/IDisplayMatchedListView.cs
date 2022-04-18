using System;

namespace BattleTechTracking.Models
{
    public interface IDisplayMatchedListView : IDisplayListView
    {
        string UnitAction { get; set; }
        EventHandler Invalidated { get; set; }
    }
}
