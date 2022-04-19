using BattleTechTracking.Factories;

namespace BattleTechTracking.Models
{
    public class Equipment : BaseModel, IDamageableComponent
    {
        private int _hits;
        private string _location;
        private string _cachedLocation;

        public string Name { get; set; }

        public int Hits
        {
            get => _hits;
            set
            {
                if (_hits == 0 && value > 0) Location = _cachedLocation;
                _hits = value;
                if (_hits < 0) _hits = 0;
                OnPropertyChanged(nameof(Hits));

                if (_hits == 0) DestroyItem();
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                if (_location == value) return;
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public virtual Equipment Copy()
            => new Equipment()
            {
                Name = this.Name,
                Hits = this.Hits,
                Location = this.Location
            };

        /// <summary>
        /// Will cache the current Location into memory.
        /// </summary>
        public void CacheLocation()
        {
            _cachedLocation = Location;
        }

        public void DestroyItem()
        {
            Location = UnitStatusFactory.DESTROYED;
        }
    }
}
