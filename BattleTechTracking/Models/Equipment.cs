namespace BattleTechTracking.Models
{
    public class Equipment : BaseModel
    {
        public string Name { get; set; }
        public int Hits { get; set; }
        public string Location { get; set; }
    }
}
