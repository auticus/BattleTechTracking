namespace BattleTechTracking.Models
{
    public class Ammunition : Equipment
    {
        private int _ammoCount;
        private bool _hasCASE;
        private const int CASE_DIVIDER = 20;
        private const int DEFAULT_DIVIDER = 10;

        /// <summary>
        /// Gets or sets a value indicating if the mech has CASE installed.
        /// </summary>
        public bool HasCASE
        {
            get => _hasCASE;
            set
            {
                _hasCASE = value;
                OnPropertyChanged(nameof(HasCASE));
                OnPropertyChanged(nameof(AmmoExplosionDamage));
            }
        }

        public int AmmoCount
        {
            get => _ammoCount;
            set
            {
                _ammoCount = value;
                OnPropertyChanged(nameof(AmmoCount));
                OnPropertyChanged(nameof(AmmoExplosionDamage));
            }
        }

        public int WeaponDamage { get; set; }

        public int AmmoExplosionDamage
        {
            get
            {
                var baseDmg = AmmoCount * WeaponDamage;
                var divider = HasCASE ? CASE_DIVIDER : DEFAULT_DIVIDER;
                return baseDmg / divider;
            }
        } 
    }
}
