using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    internal class ClusterHitsTable : BaseChart
    {
        private const int FULL_COL_SPAN = 8;

        public ClusterHitsTable()
        {
            LoadEntries();
        }

        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "CLUSTER HITS TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2", "1", "1", "1", "2", "3", "5", "6" });
            ChartEntries.Add(new[] { "3", "1", "2", "2", "2", "3", "5", "6" });
            ChartEntries.Add(new[] { "4", "1", "2", "2", "3", "4", "6", "9" });
            ChartEntries.Add(new[] { "5", "1", "2", "3", "3", "6", "9", "12" });
            ChartEntries.Add(new[] { "6", "1", "2", "3", "4", "6", "9", "12" });
            ChartEntries.Add(new[] { "7", "1", "3", "3", "4", "6", "9", "12" });
            ChartEntries.Add(new[] { "8", "2", "3", "3", "4", "6", "9", "12" });
            ChartEntries.Add(new[] { "9", "2", "3", "4", "5", "8", "12", "16" });
            ChartEntries.Add(new[] { "10", "2", "3", "4", "5", "8", "12", "16" });
            ChartEntries.Add(new[] { "11", "2", "4", "5", "6", "10", "15", "20" });
            ChartEntries.Add(new[] { "12", "2", "4", "5", "6", "10", "15", "20" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 100, 50, 50, 50, 50, 50, 50, 50 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (2d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "2", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "4", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "5", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "6", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "10", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "15", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "20", row, col, Color.Gold);
        }
    }
}
