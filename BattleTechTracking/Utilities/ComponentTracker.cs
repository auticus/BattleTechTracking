using System.Linq;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Utility class that keeps track of component status.
    /// </summary>
    public static class ComponentTracker
    {
        private const string SHOULDER = "shoulder";
        private const string LOWER_ARM_ACTUATOR = "lower arm actuator";
        private const string UPPER_ARM_ACTUATOR = "upper arm actuator";
        /// <summary>
        /// Determines if the Sensors equipment slot is listed as damaged.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool AreSensorsDamaged(IComponentTrackable element)
        {
            var sensors = element.UnitEquipment.FirstOrDefault(p => p.Name.ToLower().Contains("sensors"));
            if (sensors == null) return false;
            return sensors.Hits < sensors.OriginalHits;
        }

        /// <summary>
        /// Determines if ANY lower, upper, or shoulder actuators are damaged.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool AreArmsOrShouldersDamaged(IComponentTrackable element)
        {
            return element.UnitEquipment.Where(DoesElementContainArmData)
                .Any(equipment => equipment.Hits < equipment.OriginalHits);
        }

        private static bool DoesElementContainArmData(Equipment equipment)
        {
            return equipment.Name.ToLower().Contains(SHOULDER) ||
                   equipment.Name.ToLower().Contains(LOWER_ARM_ACTUATOR) ||
                   equipment.Name.ToLower().Contains(UPPER_ARM_ACTUATOR);
        }
    }
}
