using System.Collections.Generic;

namespace BattleTechTracking.Models
{
    public class MatchState
    {
        public List<IDisplayMatchedListView>[] Factions { get; set; }
        public string Faction1Name { get; set; }
        public string Faction2Name { get; set; }
    }
}
