using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public class CombatVehicleCriticalHitsTable : BaseChart
    {
        private const int FULL_COL_SPAN = 5;

        public CombatVehicleCriticalHitsTable()
        {
            LoadEntries();
        }

        /// <inheritdoc/>
        public override Grid GenerateChart()
        {
            const int startingRow = 2;
            var grid = GenerateGridAndRowColumnDefinitions(DefineChart());
            InjectChartHeader(grid, "COMBAT VEHICLE CRITICAL HITS TABLE (p.194/TW)", FULL_COL_SPAN, Color.Gold);
            InjectColumnTextDefinitions(grid);
            InjectBody(grid, startingRow);
            InjectFooter(grid);
            return grid;
        }

        private void LoadEntries()
        {
            ChartEntries.Add(new[] { "2-5", "None", "None", "None", "None" });
            ChartEntries.Add(new[] { "6", "Driver Hit", "Cargo/Infantry Hit", "Weapon Malfunction", "Stabilizer" });
            ChartEntries.Add(new[] { "7", "Weapon Malfunction", "Weapon Malfunction", "Cargo/Infantry Hit", "Turret Jam" });
            ChartEntries.Add(new[] { "8", "Stabilizer", "Crew Stunned", "Stabilizer", "Weapon Malfunction" });
            ChartEntries.Add(new[] { "9", "Sensors", "Stabilizer", "Weapon Destroyed", "Turret Locks" });
            ChartEntries.Add(new[] { "10", "Commander Hit", "Weapon Destroyed", "Engine Hit", "Weapon Destroyed" });
            ChartEntries.Add(new[] { "11", "Weapon Destroyed", "Engine Hit", "Ammunition**", "Ammunition**" });
            ChartEntries.Add(new[] { "12", "Crew Killed", "Fuel Tank*", "Fuel Tank*", "Turret Popped Off" });
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
            InjectLabelInColumn(grid, "Turret", row, col, Color.Gold);
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
