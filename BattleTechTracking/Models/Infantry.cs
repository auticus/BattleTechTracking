namespace BattleTechTracking.Models
{
    public class Infantry : BaseModel, IDisplayInfantry
    {
        public string Name { get; set; }
        public string Weapon { get; set; }
        public string Range { get; set; }
        public string ArmorType { get; set; }
        public int Number { get; set; }
        public double Tonnage { get; set; }
        public int Movement { get; set; }
        public int BattleValue { get; set; }
    }
}
