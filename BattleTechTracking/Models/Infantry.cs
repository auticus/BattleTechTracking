namespace BattleTechTracking.Models
{
    public class Infantry : BaseModel, IDisplayInfantry, IDisplayListView
    {
        public string Name { get; set; }
        public string Weapon { get; set; }
        public string Range { get; set; }
        public string ArmorType { get; set; }
        public int Number { get; set; }
        public double Tonnage { get; set; }
        public int Movement { get; set; }
        public int BattleValue { get; set; }

        public string UnitHeader => $"{Name} ({Weapon})";
        public string UnitDetails => $"{Number} soldiers - BV: {BattleValue}";
    }
}
