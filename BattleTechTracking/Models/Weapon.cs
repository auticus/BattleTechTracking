using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BattleTechTracking.Models
{
    public enum WeaponFiringStatus
    {
        NotFired = 0,
        WeaponFired,
        WeaponDestroyed,
        OutOfAmmo
    }

    public class Weapon : Equipment
    {
        private WeaponFiringStatus _weaponFiringStatus;

        public int Heat { get; set; }
        public int Damage { get; set; }
        public IEnumerable<string> DamageCodes { get; set; } = new List<string>();
        public string DamageCodesText => string.Join(", ", DamageCodes.Select(code => code));
        public int MinimumRange { get; set; }
        public int ShortRange { get; set; }
        public int MediumRange { get; set; }
        public int LongRange { get; set; }
        public bool UsesAmmunition { get; set; }

        [JsonIgnore]
        public WeaponFiringStatus WeaponFiringStatus
        {
            get => _weaponFiringStatus;
            set
            {
                _weaponFiringStatus = value;
                OnPropertyChanged(nameof(WeaponFiringStatus));
            }
        }
        public IEnumerable<Ammunition> Ammo { get; set; } = new List<Ammunition>();

        public override void DestroyItem()
        {
            base.DestroyItem();
            WeaponFiringStatus = WeaponFiringStatus.WeaponDestroyed;
        }

        public override bool TryRestoreItem()
        {
            if (!base.TryRestoreItem()) return false;
            WeaponFiringStatus = WeaponFiringStatus.NotFired;
            return true;
        }

        public override string ToString() => Name;
    }
}
