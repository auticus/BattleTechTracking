using System.Collections.Generic;

namespace BattleTechTracking.Models
{
    public class MatchState
    {
        public const MatchState NoMatchStateLoaded = null;

        public IList<IDisplayMatchedListView>[] Factions { get; set; }
        public string Faction1Name { get; set; }
        public string Faction2Name { get; set; }
    }
}
