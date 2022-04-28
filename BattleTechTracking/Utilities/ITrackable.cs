using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Interface applied to any element that can be tracked by the <see cref="GameStateTracker"/>
    /// </summary>
    public interface ITrackable
    {
        int HexesMoved { get; set; }
        bool DidWalk { get; set; }
        bool DidRun { get; set; }
        bool DidJump { get; set; }
        string UnitAction { get; set; }
        IEnumerable<Weapon> UnitWeapons { get; }
    }
}
