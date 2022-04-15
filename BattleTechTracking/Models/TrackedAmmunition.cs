namespace BattleTechTracking.Models
{
    public class TrackedAmmunition : BaseModel
    {
        private int _hitsTaken;
        private int _currentAmmoCount;

        public Ammunition TemplatedAmmunition { get; }

        public int HitsTaken
        {
            get => _hitsTaken;
            set
            {
                _hitsTaken = value;
                OnPropertyChanged(nameof(HitsTaken));
            }
        }

        public int CurrentAmmoCount
        {
            get => _currentAmmoCount;
            set
            {
                _currentAmmoCount = value;
                OnPropertyChanged(nameof(CurrentAmmoCount));
            }
        }

        public TrackedAmmunition(Ammunition baseAmmunition)
        {
            TemplatedAmmunition = baseAmmunition;
        }
    }
}
