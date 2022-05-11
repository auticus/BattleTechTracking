using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class FacingAfterFallTable : BaseChart
    {
        private const int FULL_COL_SPAN = 3;

        public FacingAfterFallTable()
        {
            LoadEntries();
        }

        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "FACING AFTER FALL TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "1", "Same Direction", "Front" });
            ChartEntries.Add(new[] { "2", "1 Hexside Right", "Right Side" });
            ChartEntries.Add(new[] { "3", "2 Hexside Right", "Right Side" });
            ChartEntries.Add(new[] { "4", "Opposite Direction", "Rear" });
            ChartEntries.Add(new[] { "5", "2 Hexside Left", "Left Side" });
            ChartEntries.Add(new[] { "6", "1 Hexside Left", "Left Side" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 200, 200, 200 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (1d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "New Facing", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Hit Location", row, col, Color.Gold);
        }
    }
}
