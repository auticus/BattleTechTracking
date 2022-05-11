using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class KickLocationTable : BaseChart
    {
        private const int FULL_COL_SPAN = 4;

        public KickLocationTable()
        {
            LoadEntries();
        }

        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "KICK LOCATION TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "1-3", "Left Leg", "Right Leg", "Right Leg" });
            ChartEntries.Add(new[] { "4-6", "Left Leg", "Left Leg", "Right Leg" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25 };
            var cols = new[] { 150, 150, 150, 150 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (1d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Left Side", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Front/Rear", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Right Side", row, col, Color.Gold);
        }
    }
}
