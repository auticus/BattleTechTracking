namespace BattleTechTracking.Models
{
    public class Equipment : BaseModel, IDamageableComponent
    {
        private int _hits;

        public string Name { get; set; }

        public int Hits
        {
            get => _hits;
            set
            {
                _hits = value;
                OnPropertyChanged(nameof(Hits));
            }
        }
        
        public string Location { get; set; }

        public virtual Equipment Copy()
            => new Equipment()
            {
                Name = this.Name,
                Hits = this.Hits,
                Location = this.Location
            };
    }
}
