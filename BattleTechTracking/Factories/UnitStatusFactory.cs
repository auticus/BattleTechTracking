using System.Collections.Generic;

namespace BattleTechTracking.Factories
{
    public class UnitStatusFactory
    {
        public static IEnumerable<string> BuildStatusList()
            => new List<string>()
            {
                EquipmentStatus.UNDAMAGED,
                EquipmentStatus.DAMAGED,
                EquipmentStatus.CRIPPLED,
                EquipmentStatus.DESTROYED
            };
    }
}
