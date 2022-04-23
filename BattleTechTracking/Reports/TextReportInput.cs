using System.Collections.Generic;
using System.Linq;
using BattleTechTracking.Models;

namespace BattleTechTracking.Reports
{
    public class TextReportInput
    {
        public IList<IReportable>[] FactionUnitData { get; }
        public IList<string> FactionNames { get; }

        public TextReportInput(IList<IReportable>[] factionUnitData,
                                IList<string> factionNames)
        {
            FactionUnitData = factionUnitData;
            FactionNames = factionNames;
        }

        public static IList<IReportable>[] ConvertFactionDataToReportableFormat(IList<IDisplayMatchedListView>[] data)
        {
            var convertedList = new IList<IReportable>[2];
            var factionIndex = 0;
            foreach (var faction in data)
            {
                var factionList = faction.Cast<IReportable>().ToList();
                convertedList[factionIndex] = factionList;

                factionIndex++;
            }

            return convertedList;
        }
    }
}
