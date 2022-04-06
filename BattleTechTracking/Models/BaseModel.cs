using System;
using System.ComponentModel;

namespace BattleTechTracking.Models
{
    /// <summary>
    /// Base model that allows for notifications when property changes.
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public Guid ID { get; }
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
