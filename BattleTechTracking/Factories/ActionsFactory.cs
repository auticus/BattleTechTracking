using System.Collections.Generic;

namespace BattleTechTracking.Factories
{
    public static class ActionsFactory
    {
        public const string NO_ACTION = "No Action Taken";
        public const string MOVED = "Moved";
        public const string WEAPONS_SHOT = "Weapons Shot";
        public const string MELEE = "Melee";

        public static IEnumerable<string> BuildActionsList()
            => new List<string>()
                {
                    NO_ACTION,
                    MOVED,
                    WEAPONS_SHOT,
                    MELEE
                };
        
    }
}
