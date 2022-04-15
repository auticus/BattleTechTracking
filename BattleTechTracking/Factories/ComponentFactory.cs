using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class ComponentFactory
    {
        public static UnitComponent BuildComponentFromTemplate(UnitComponent template)
            => new UnitComponent()
            {
                Armor = template.Armor, 
                Name = template.Name, 
                RearArmor = template.RearArmor,
                Structure = template.Structure
            };
    }
}
