using System.Linq;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Utility class used for handling Heat
    /// </summary>
    public static class Heat
    {
        /// <summary>
        /// Performs end of round heat calculations.
        /// </summary>
        /// <param name="element"></param>
        public static void DoEndOfRoundHeatCalculation(IHeatable element)
        {
            if (!ElementTracksHeat(element)) return;

            element.CurrentHeatLevel -= element.CurrentHeatSinks;
            if (element.CurrentHeatLevel < 0) element.CurrentHeatLevel = 0;

            var engineHitsTaken = GetEngineDamage(element);
            if (engineHitsTaken == 0) return;

            if (engineHitsTaken == 1) element.CurrentHeatLevel += 5;
            else if (engineHitsTaken >= 2) element.CurrentHeatLevel += 10;
        }

        /// <summary>
        /// Returns a value indicating if the element is capable of tracking heat.
        /// </summary>
        /// <param name="element"></param>
        /// <returns>A value indicating if the element is capable of tracking heat.</returns>
        public static bool ElementTracksHeat(IHeatable element)
        {
            if (element.GameElement.GetType() == typeof(BattleMech)) return true;
            return element.GameElement.GetType() == typeof(IndustrialMech);
        }

        private static int GetEngineDamage(IHeatable element)
        {
            var engine = element.UnitEquipment.FirstOrDefault(equip => equip.Name.ToLower().Contains("engine"));
            if (engine == null) return 0;

            return engine.OriginalHits - engine.Hits;
        }
    }
}
