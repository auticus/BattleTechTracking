namespace BattleTechTracking.Models
{
    public struct UnitComponent
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public int? RearArmor { get; set; }
        public int Structure { get; set; }
    }
}
