namespace BattleTechTracking.Models
{
    public class ComponentCombatModifier
    {
        public string Component { get; }
        public string Description { get; }
        public int CombatRoll { get; }

        public ComponentCombatModifier(string component, string description, int combatRoll)
        {
            this.Component = component;
            this.Description = description;
            this.CombatRoll = combatRoll;
        }
    }
}
