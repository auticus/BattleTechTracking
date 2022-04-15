using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class InfantryFactory
    {
        public static TrackedGameElement BuildTrackedGameElement(Infantry template)
        {
            return new TrackedGameElement(BuildInfantryFromTemplate(template));
        }
        private static Infantry BuildInfantryFromTemplate(Infantry template)
        {
            return new Infantry()
            {
                Name = template.Name,
                ArmorType = template.ArmorType,
                BattleValue = template.BattleValue,
                Movement = template.Movement,
                Number = template.Number,
                Range = template.Range,
                Tonnage = template.Tonnage,
                Weapon = template.Weapon
            };
        }
    }
}
