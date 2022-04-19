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

            foreach(var component in vehicle.Components) component.SetOriginalValuesFromCurrentValues();
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
                //note: engines can be hurt from the side or rear but just putting it in here to have a location
                new Equipment(){Name = "ICE Engine", Hits=1, Location=UnitComponent.REAR_SIDE_CODE},
                new Equipment(){Name = "Turret", Hits=1, Location=UnitComponent.TURRET_CODE},
                new Equipment(){Name = "Sensors", Hits=4, Location=UnitComponent.FRONT_CODE},
                new Equipment(){Name = "Motive System", Hits=3, Location=UnitComponent.ALL_VEHICLE},
                new Equipment(){Name = "Front Stabalizer", Hits=1, Location=UnitComponent.FRONT_CODE},
                new Equipment(){Name = "Rear Stabalizer", Hits=1, Location=UnitComponent.REAR_CODE},
                new Equipment(){Name = "Left Stabalizer", Hits=1, Location= UnitComponent.LEFT_SIDE_CODE},
                new Equipment(){Name = "Right Stabalizer", Hits=1, Location=UnitComponent.RIGHT_SIDE_CODE},
                new Equipment(){Name = "Turret Stabalizer", Hits=1, Location = UnitComponent.FRONT_CODE}
            };
        }
    }
}
