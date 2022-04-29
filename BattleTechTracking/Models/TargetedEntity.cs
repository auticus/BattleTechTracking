namespace BattleTechTracking.Models
{
    public class TargetedEntity
    {
        public string Header { get; }

        /// <summary>
        /// Gets the required gunnery score base to hit.
        /// </summary>
        public int GunneryScore { get; }

        public bool IsProne { get; }
        public string ProneCaveat => IsProne ? "*TARGET PRONE - Subtract 3 from given score if adjacent" : string.Empty;

        public TargetedEntity(string header, int gunneryScore, bool isProne)
        {
            this.Header = header;
            this.GunneryScore = gunneryScore;
            this.IsProne = isProne;
        }

        public override string ToString() => Header;
    }
}
