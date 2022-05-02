using System;
using BattleTechTracking.Factories;

namespace BattleTechTracking.Models
{
    public class Equipment : BaseModel, IDamageableComponent
    {
        private int _hits;
        private string _location;
        private string _originalLocation;
        private int _originalHits;
        private string _name;

        /// <summary>
        /// Fires whenever a piece of equipment has 0 hits and the hits are made positive again.
        /// </summary>
        public EventHandler OnEquipmentAttemptedToBeRestored { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value.Trim();
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Hits
        {
            get => _hits;
            set
            {
                //model starts at 0 and gets a value populated for it - so cache that
                if (_hits == 0 && value > 0 && string.IsNullOrEmpty(_originalLocation))
                {
                    Location = _originalLocation;
                    OriginalHits = value;
                }
                else if (_hits == 0 && value > 0)
                {
                    OnEquipmentAttemptedToBeRestored?.Invoke(this, EventArgs.Empty);
                }

                _hits = value;
                if (_hits < 0) _hits = 0;
                OnPropertyChanged(nameof(Hits));

                if (_hits == 0) DestroyItem();
            }
        }


        public int OriginalHits
        {
            get => _originalHits;
            set
            {
                _originalHits = value;
                OnPropertyChanged(nameof(OriginalHits));
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

        public string OriginalLocation => _originalLocation;

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
            _originalLocation = Location;
        }

        public virtual void DestroyItem()
        {
            Location = EquipmentStatus.DESTROYED;
        }

        public virtual bool TryRestoreItem()
        {
            if (Hits == 0) return false;
            Location = _originalLocation;
            return true;
        }

        public virtual void ForceRestoreItem()
        {
            Location = _originalLocation;
        }
    }
}
