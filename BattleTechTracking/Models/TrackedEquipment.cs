namespace BattleTechTracking.Models
{
    public class TrackedEquipment : BaseModel
    {
        private int _hitsTaken;
        public Equipment TemplatedEquipment { get; }

        public int HitsTaken
        {
            get => _hitsTaken;
            set
            {
                _hitsTaken = value;
                OnPropertyChanged(nameof(HitsTaken));
            }
        }

        public TrackedEquipment(Equipment baseEquipment)
        {
            TemplatedEquipment = baseEquipment;
        }
    }
}
