using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class CombatVehicleFactory
    {
        public static CombatVehicle BuildDefaultCombatVehicle()
        {
            var vehicle = new CombatVehicle
            {
                Name = "Unnamed",
                Tonnage = 10,
                Model = "Unknown",
                TechBase = "Inner Sphere",
                RulesLevel = "Introductory",
                UnitMovement =
                {
                    Walking = 5,
                    Running = 5
                },
                Components = GetBasicVehicleLocations(),
                Equipment = GetBasicStandardEquipmentLoadout()
            };

            return vehicle;
        }

        public static TrackedGameElement BuildTrackedGameElement(CombatVehicle template)
        {
            return new TrackedGameElement(BuildCombatVehicleFromTemplate(template));
        }

        private static CombatVehicle BuildCombatVehicleFromTemplate(CombatVehicle template)
        {
            var vehicle = new CombatVehicle
            {
                Name = template.Name,
                Tonnage = template.Tonnage,
                Model = template.Model,
                TechBase = template.TechBase,
                RulesLevel = template.RulesLevel,
                BattleValue = template.BattleValue,
                UnitMovement =
                {
                    Walking = template.UnitMovement.Walking,
                    Running = template.UnitMovement.Running,
                    Jumping = template.UnitMovement.Jumping
                },
                Components = new List<UnitComponent>(template.Components),
                Equipment = new List<Equipment>(template.Equipment),
                Weapons = new List<Weapon>(template.Weapons),
                Quirks = new List<Quirk>(template.Quirks)
            };

            return vehicle;
        }

        private static IEnumerable<UnitComponent> GetBasicVehicleLocations()
        {
            return new List<UnitComponent>
            {
                new UnitComponent(){Name = "Front"},
                new UnitComponent(){Name = "Right Side"},
                new UnitComponent(){Name = "Left Side"},
                new UnitComponent(){Name = "Rear"},
                new UnitComponent(){Name = "Turret"}
            };
        }

        private static IEnumerable<Equipment> GetBasicStandardEquipmentLoadout()
        {
            return new List<Equipment>
            {
                new Equipment(){Name = "ICE Engine", Hits=1, Location="R"},
                new Equipment(){Name = "Turret", Hits=1, Location="TU"},
                new Equipment(){Name = "Sensors", Hits=4, Location=""},
                new Equipment(){Name = "Motive System", Hits=3, Location=""},
                new Equipment(){Name = "Front Stabalizer", Hits=1, Location="F"},
                new Equipment(){Name = "Rear Stabalizer", Hits=1, Location="R"},
                new Equipment(){Name = "Left Stabalizer", Hits=1, Location="LS"},
                new Equipment(){Name = "Right Stabalizer", Hits=1, Location="RS"},
                new Equipment(){Name = "Turret Stabalizer", Hits=1, Location="F"}
            };
        }
    }
}
