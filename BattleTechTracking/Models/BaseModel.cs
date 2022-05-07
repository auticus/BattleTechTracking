using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Base model that allows for notifications when property changes.
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        private bool _isSelected;

        public Guid ID { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseModel()
        {
            ID = Guid.NewGuid();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
