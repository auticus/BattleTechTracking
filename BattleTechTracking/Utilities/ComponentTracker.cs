using System.Linq;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Utility class that keeps track of component status.
    /// </summary>
    public static class ComponentTracker
    {
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
        public static bool AreAnyArmsOrShouldersDamaged(IComponentTrackable element)
        {
            var armData = element.UnitEquipment.Where(DoesElementContainArmData);
            foreach (var arm in armData)
            {
                if (arm.Hits < arm.OriginalHits) return true;
                if (arm.Location == EquipmentStatus.DESTROYED) return true;
            }

            return false;
        }

        private static bool DoesElementContainArmData(Equipment equipment)
        {
            return equipment.Name.ToLower().Contains(UnitComponent.SHOULDER) ||
                   equipment.Name.ToLower().Contains(UnitComponent.LOWER_ARM_ACTUATOR) ||
                   equipment.Name.ToLower().Contains(UnitComponent.UPPER_ARM_ACTUATOR);
        }
    }
}
