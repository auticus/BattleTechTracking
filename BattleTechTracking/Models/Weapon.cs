using System.Collections.Generic;
using System.Linq;

namespace BattleTechTracking.Models
{
    public class Weapon : Equipment
    {
        public int Heat { get; set; }
        public int Damage { get; set; }
        public IEnumerable<string> DamageCodes { get; set; } = new List<string>();
        public int MinimumRange { get; set; }
        public int ShortRange { get; set; }
        public int MediumRange { get; set; }
        public int LongRange { get; set; }
        public IEnumerable<Ammunition> Ammo { get; set; } = new List<Ammunition>();

        public new Weapon Copy()
            => new Weapon
            {
                Name = this.Name,
                Location = this.Location,
                Heat = this.Heat,
                Damage = this.Damage,
                MinimumRange = this.MinimumRange,
                ShortRange = this.ShortRange,
                MediumRange = this.MediumRange,
                LongRange = this.LongRange,
                DamageCodes = new List<string>(this.DamageCodes),
                Ammo = new List<Ammunition>(this.Ammo),
                Hits = this.Hits
            };
    }
}
