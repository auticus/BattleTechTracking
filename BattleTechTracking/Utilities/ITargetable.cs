namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Interface applied to elements that can be targetable by weapons.
    /// </summary>
    public interface ITargetable
    {
        int HexesMoved { get; }
        bool DidJump { get; }
        bool IsProne { get; }
        string UnitHeader { get; }
        string PilotName { get; }
    }
}
