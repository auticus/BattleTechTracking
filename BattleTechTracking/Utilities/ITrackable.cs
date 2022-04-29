using System.Collections.ObjectModel;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Interface applied to any element that can be tracked by the <see cref="GameStateTracker"/>
    /// </summary>
    public interface ITrackable
    {
        IDisplayListView GameElement { get; }
        int HexesMoved { get; set; }
        bool DidWalk { get; set; }
        bool DidRun { get; set; }
        bool DidJump { get; set; }
        string UnitAction { get; set; }
        ObservableCollection<Weapon> UnitWeapons { get; }
    }
}
