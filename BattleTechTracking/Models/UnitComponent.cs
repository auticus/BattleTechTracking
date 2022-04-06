using System;

namespace BattleTechTracking.Models
{
    public class UnitComponent : BaseModel
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public int? RearArmor { get; set; }
        public int Structure { get; set; }
    }
}
