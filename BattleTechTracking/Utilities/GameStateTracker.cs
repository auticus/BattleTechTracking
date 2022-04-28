using System.Collections.Generic;
using System.Linq;
using BattleTechTracking.Factories;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Utility class that tracks the state of the game and provides helper functions for tracking elements.
    /// </summary>
    public static class GameStateTracker
    {
        /// <summary>
        /// Resets an element for the beginning of a new turn.
        /// </summary>
        public static void NextRound(IEnumerable<ITrackable> elements)
        {
            foreach (var element in elements)
            {
                element.HexesMoved = 0;
                element.DidWalk = false;
                element.DidRun = false;
                element.DidJump = false;
                //prone intentionally not reset

                if (element is IHeatable heatable) Heat.DoEndOfRoundHeatCalculation(heatable);
                element.UnitAction = ActionsFactory.NO_ACTION;
                foreach (var wpn in element.UnitWeapons.Where(p => p.WeaponFiringStatus != WeaponFiringStatus.WeaponDestroyed))
                {
                    wpn.WeaponFiringStatus = WeaponFiringStatus.NotFired;
                }
            }
        }
    }
}
