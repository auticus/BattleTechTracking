namespace BattleTechTracking.Models
{
    public class TargetedEntity
    {
        public string Header { get; }
        public int Modifier { get; }

        public TargetedEntity(string header, int modifier)
        {
            this.Header = header;
            this.Modifier = modifier;
        }

        public override string ToString() => Header;
    }
}
