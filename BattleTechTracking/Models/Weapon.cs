using System.Collections.Generic;
using System.Linq;

namespace BattleTechTracking.Models
{
    public class Weapon : Equipment
    {
        public int Heat { get; set; }
        public int Damage { get; set; }
        public IEnumerable<string> DamageCodes { get; set; } = new List<string>();
        public string DamageCodesText => string.Join(", ", DamageCodes.Select(code => code));
        public int MinimumRange { get; set; }
        public int ShortRange { get; set; }
        public int MediumRange { get; set; }
        public int LongRange { get; set; }
        public IEnumerable<Ammunition> Ammo { get; set; } = new List<Ammunition>();
    }
}
