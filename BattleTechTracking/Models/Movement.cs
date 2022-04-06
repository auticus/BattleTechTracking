namespace BattleTechTracking.Models
{
    /// <summary>
    /// Model that holds information on Movement.
    /// </summary>
    public class Movement : BaseModel
    {
        public int Walking { get; set; }
        public int Running { get; set; }
        public int Jumping { get; set; }
    }
}
