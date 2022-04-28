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
        public const string DEFAULT_PILOT_NAME = "Unknown";
        private const int MECH_WARRIOR_STARTING_HITS = 6;
        private const int COMBAT_VEHICLE_STARTING_HITS = 1;
        private const int NON_INFANTRY_DEFAULT_NUMBER_OF_ELEMENTS = 1;
        private const int NONE = 0;

        public static int GetHeatSinksFromElement(ITrackable gameElement)
        {
            if (!(gameElement is BattleMech element))
            {
                element = gameElement as IndustrialMech;
            }

            return element?.HeatSinks ?? NONE;
        }

        public static int GetNumberOfElementsFromGameElement(ITrackable gameElement)
        {
            if (!(gameElement is Infantry element))
            {
                return NON_INFANTRY_DEFAULT_NUMBER_OF_ELEMENTS;
            }

            return element.Number;
        }

        public static int GetStartingHitsForPilot(ITrackable gameElement)
        {
            switch (gameElement.GameElement)
            {
                case BattleMech _:
                    return MECH_WARRIOR_STARTING_HITS;
                case CombatVehicle _:
                    return COMBAT_VEHICLE_STARTING_HITS;
            }

            return 0;
        }

        /// <summary>
        /// Resets an element for the beginning of a new turn.
        /// </summary>
        public static void NextRound(IEnumerable<ITrackable> elements)
        {
            foreach (var element in elements)
            {
                element.HexesMoved = NONE;
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
