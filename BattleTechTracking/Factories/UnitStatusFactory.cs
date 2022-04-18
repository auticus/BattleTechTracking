using System.Collections.Generic;

namespace BattleTechTracking.Factories
{
    public class UnitStatusFactory
    {
        public const string UNDAMAGED = "Undamaged";
        public const string DAMAGED = "Damaged";
        public const string CRIPPLED = "Crippled";
        public const string DESTROYED = "Destroyed";

        public static IEnumerable<string> BuildStatusList()
            => new List<string>()
            {
                UNDAMAGED,
                DAMAGED,
                CRIPPLED,
                DESTROYED
            };
    }
}
