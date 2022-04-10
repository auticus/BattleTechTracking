namespace BattleTechTracking.Models
{
    public class Equipment : BaseModel
    {
        public string Name { get; set; }
        public int Hits { get; set; }
        public string Location { get; set; }

        public Equipment Copy()
            => new Equipment()
            {
                Name = this.Name,
                Hits = this.Hits,
                Location = this.Location
            };
    }
}
