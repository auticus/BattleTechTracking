using System.Collections.Generic;
using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class DetermineCritHitsTable : BaseChart
    {
        private const int FULL_COL_SPAN = 2;

        public DetermineCritHitsTable()
        {
            LoadEntries();
        }

        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "DETERMINING CRIT HITS TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2-7", "No Crit Hit" });
            ChartEntries.Add(new[] { "8-9", "Roll 1 Crit Location" });
            ChartEntries.Add(new[] { "10-11", "Roll 2 Crit Locations" });
            ChartEntries.Add(new[] { "12", "Head/Limb blown off*" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 200, 200 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (2d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Effect", row, col, Color.Gold);
        }

        private void InjectFooter(Grid grid)
        {
            const int row = 6;
            InjectLabelInFooter(grid, "*Roll 3 crit hits if the attack strikes the torso.",
                row,
                Color.Gold,
                FULL_COL_SPAN);
        }
    }
}
