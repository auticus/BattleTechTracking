using System;

namespace BattleTechTracking.Models
{
    public class TrackedWeapon : BaseModel
    {
        private int _hitsTaken;
        private bool _didShoot;

        public Weapon TemplatedWeapon { get; }

        public EventHandler<int> OnHeatGenerated { get; set; }

        public int HitsTaken
        {
            get => _hitsTaken;
            set
            {
                _hitsTaken = value;
                OnPropertyChanged(nameof(HitsTaken));
            }
        }

        public bool DidShoot
        {
            get => _didShoot;
            set
            {
                _didShoot = value;
                OnPropertyChanged(nameof(DidShoot));

                OnHeatGenerated?.Invoke(this, TemplatedWeapon.Heat);
            }
        }

        public TrackedWeapon(Weapon baseWeapon)
        {
            TemplatedWeapon = baseWeapon;
        }
    }
}
