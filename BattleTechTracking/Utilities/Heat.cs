using System.Linq;
using System.Text;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Enum detailing the various colors of Heat
    /// </summary>
    public enum HeatLevels
    {
        None,
        Green,
        Yellow,
        Orange,
        Red
    }

    /// <summary>
    /// Utility class used for handling Heat
    /// </summary>
    public static class Heat
    {
        public const int MAX_HEAT = 99;
        /// <summary>
        /// Performs end of round heat calculations.
        /// </summary>
        /// <param name="element"></param>
        public static void DoEndOfRoundHeatCalculation(IHeatable element)
        {
            if (!ElementTracksHeat(element)) return;

            var engineHitsTaken = GetEngineDamage(element);
            if (engineHitsTaken == 1) element.CurrentHeatLevel += 5;
            else if (engineHitsTaken >= 2) element.CurrentHeatLevel += 10;

            element.CurrentHeatLevel -= element.CurrentHeatSinks;
            if (element.CurrentHeatLevel < 0) element.CurrentHeatLevel = 0;
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

        public static HeatLevels GetHeatMapColorForLevel(int heatLevel)
        {
            if (heatLevel < 5) return HeatLevels.Green;
            if (heatLevel < 15) return HeatLevels.Yellow;
            if (heatLevel < 25) return HeatLevels.Orange;
            return HeatLevels.Red;
        }

        public static string GetHeatImpactTooltip(IHeatable element)
        {
            // note this only works for mechs and assumes all mechs have the same chart for heat level
            var movementMod = GetMovementModifierFromHeat(element.CurrentHeatLevel);
            var firingMod = GetFireModifierFromHeat(element.CurrentHeatLevel);
            var shutDownMod = GetShutDownScoreFromHeat(element.CurrentHeatLevel);
            var ammoExplosionMod = GetAmmoExplosionScoreFromHeat(element.CurrentHeatLevel);
            var engineHitsTaken = GetEngineDamage(element);

            if (movementMod == 0 && engineHitsTaken == 0) return string.Empty;

            var sb = new StringBuilder();
            if (engineHitsTaken > 0) sb.Append(GetEngineDamageDescription(engineHitsTaken));
            if (movementMod > 0) sb.Append(GetMovementDescription(movementMod));
            if (firingMod > 0) sb.Append($"; {GetFireModDescription(firingMod)}");
            if (shutDownMod > 0) sb.Append($"; {GetShutDownDescription(shutDownMod)}");
            if (ammoExplosionMod > 0) sb.Append($"; {GetAmmoExplosionDescription(ammoExplosionMod)}");
            return sb.ToString();
        }

        private static int GetEngineDamage(IHeatable element)
        {
            var engine = element.UnitEquipment.FirstOrDefault(equip => equip.Name.ToLower().Contains("engine"));
            if (engine == null) return 0;

            return engine.OriginalHits - engine.Hits;
        }

        private static int GetMovementModifierFromHeat(int heatLevel)
        {
            if (heatLevel < 5) return 0;
            if (heatLevel < 10) return 1;
            if (heatLevel < 15) return 2;
            if (heatLevel < 20) return 3;
            if (heatLevel < 25) return 4;
            return 5;
        }

        private static int GetFireModifierFromHeat(int heatLevel)
        {
            if (heatLevel < 8) return 0;
            if (heatLevel < 12) return 1;
            if (heatLevel < 17) return 2;
            if (heatLevel < 24) return 3;
            return 4;
        }

        private static int? GetShutDownScoreFromHeat(int heatLevel)
        {
            if (heatLevel < 14) return 0;
            if (heatLevel < 18) return 4;
            if (heatLevel < 22) return 6;
            if (heatLevel < 26) return 8;
            if (heatLevel < 30) return 10;
            return null; //30 is an auto shutdown
        }

        private static int GetAmmoExplosionScoreFromHeat(int heatLevel)
        {
            if (heatLevel < 19) return 0;
            if (heatLevel < 23) return 4;
            if (heatLevel < 28) return 6;
            return 8;
        }

        private static string GetMovementDescription(int level)
        {
            if (level == 0) return string.Empty;
            return $"-{level} Movement";
        }

        private static string GetFireModDescription(int level)
        {
            if (level == 0) return string.Empty;
            return $"+{level} GunneryScore to Fire";
        }

        private static string GetShutDownDescription(int? level)
        {
            if (level == 0) return string.Empty;
            return level.HasValue ? $"Shutdown, avoid on {level}+" : "Shutdown";
        }

        private static string GetAmmoExplosionDescription(int level)
        {
            if (level == 0) return string.Empty;
            return $"Ammo Exp, avoid on {level}+";
        }

        private static string GetEngineDamageDescription(int hitsTaken)
        {
            if (hitsTaken == 0) return string.Empty;
            var extraHeat = hitsTaken == 1 ? 5 : 10;
            return $"ENGINE HIT x{hitsTaken} (+{extraHeat} heat); ";
        }
    }
}
