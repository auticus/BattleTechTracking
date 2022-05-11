using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class VTOLHitLocationTable : BaseChart
    {
        private const int FULL_COL_SPAN = 4;

        public VTOLHitLocationTable()
        {
            LoadEntries();
        }

        /// <inheritdoc/>
        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "VTOL HIT LOCATION TABLE", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2", "Front*", "Rear*", "Side*" });
            ChartEntries.Add(new[] { "3", "Rotors", "Rotors", "Rotors" });
            ChartEntries.Add(new[] { "4", "Rotors", "Rotors", "Rotors" });
            ChartEntries.Add(new[] { "5", "Right Side", "Left Side", "Front" });
            ChartEntries.Add(new[] { "6", "Front", "Rear", "Side" });
            ChartEntries.Add(new[] { "7", "Front", "Rear", "Side" });
            ChartEntries.Add(new[] { "8", "Front", "Rear", "Side*" });
            ChartEntries.Add(new[] { "9", "Left Side", "Right Side", "Rear" });
            ChartEntries.Add(new[] { "10", "Rotors", "Rotors", "Rotors" });
            ChartEntries.Add(new[] { "11", "Rotors", "Rotors", "Rotors" });
            ChartEntries.Add(new[] { "12", "Rotors*", "Rotors*", "Rotors*" });

        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 150, 150, 150, 150 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (2d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Front", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Rear", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Side", row, col, Color.Gold);
        }

        private void InjectFooter(Grid grid)
        {
            var row = 13;
            InjectLabelInFooter(grid, "*Apply damage normal then roll on crit hits table.",
                row++,
                Color.Gold,
                FULL_COL_SPAN);

            InjectLabelInFooter(grid, "Rotor hit - see p.197/TW.  Each hit reduces MP by 1.",
                row,
                Color.Gold,
                FULL_COL_SPAN);
        }
    }
}
