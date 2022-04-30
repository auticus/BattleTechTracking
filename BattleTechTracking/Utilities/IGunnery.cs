using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Interface given for an element that can target other elements.
    /// </summary>
    public interface IGunnery
    {
        int HexesMoved { get; }
        bool DidWalk { get; }
        bool DidRun { get;  }
        bool DidJump { get; }
        bool IsProne { get; }
        int PilotGunnerySkill { get; }
        int CurrentHeatLevel { get; }
    }
}
