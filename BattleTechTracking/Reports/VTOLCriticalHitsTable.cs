using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class VTOLCriticalHitsTable : BaseChart
    {
        private const int FULL_COL_SPAN = 5;

        public VTOLCriticalHitsTable()
        {
            LoadEntries();
        }

        /// <inheritdoc/>
        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "VTOL CRITICAL HITS TABLE (p.196/TW)", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2-5", "None", "None", "None", "None" });
            ChartEntries.Add(new[] { "6", "Co-Pilot Hit", "Weapon Malfunction", "Cargo/Infantry Hit", "Rotor Damage" });
            ChartEntries.Add(new[] { "7", "Weapon Malfunction", "Cargo/Infantry Hit", "Weapon Malfunction", "Rotor Damage" });
            ChartEntries.Add(new[] { "8", "Stabilizer", "Stabilizer", "Stabilizer", "Rotor Damage" });
            ChartEntries.Add(new[] { "9", "Sensors", "Weapon Destroyed", "Weapon Destroyed", "Flight Stabilizer Hit" });
            ChartEntries.Add(new[] { "10", "Pilot Hit", "Engine Damage", "Sensors", "Flight Stabilizer Hit" });
            ChartEntries.Add(new[] { "11", "Weapon Destroyed", "Ammunition**", "Engine Damage", "Rotors Destroyed" });
            ChartEntries.Add(new[] { "12", "Crew Killed", "Fuel Tank*", "Fuel Tank*", "Rotors Destroyed" });
        }

        private ChartDefinition DefineChart()
        {
            var rows = new[] { 32, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 };
            var cols = new[] { 75, 150, 150, 150, 150 };
            return new ChartDefinition(rows, cols);
        }

        private void InjectColumnTextDefinitions(Grid grid)
        {
            const int row = 1;
            var col = 0;
            InjectLabelInColumn(grid, "Roll (2d6)", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Front", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Side", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Rear", row, col++, Color.Gold);
            InjectLabelInColumn(grid, "Rotors", row, col, Color.Gold);
        }

        private void InjectFooter(Grid grid)
        {
            var row = 10;
            InjectLabelInFooter(grid, "*Only if ICE engine. Fusion engines treat as Engine Hit.",
                row++,
                Color.Gold,
                FULL_COL_SPAN);

            InjectLabelInFooter(grid, "**If no ammo, treat as Weapon Destroyed",
                row,
                Color.Gold,
                FULL_COL_SPAN);
        }
    }
}
