using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class MechFactory
    {
        public static BattleMech BuildDefaultBattleMech()
            => new BattleMech
            {
                Name = "Unnamed",
                Tonnage = 20,
                Model = "Unknown",
                TechBase = "Inner Sphere",
                RulesLevel = "Introductory",
                UnitMovement =
                {
                    Walking = 5,
                    Running = 5
                },
                Components = GetBasicBipedMechLocations(),
                Equipment = GetBasicStandardEquipmentLoadout()
            };

        public static IndustrialMech BuildDefaultIndustrialMech()
            => new IndustrialMech()
            {
                Name = "Unnamed",
                Tonnage = 20,
                Model = "Unknown",
                TechBase = "Inner Sphere",
                RulesLevel = "Introductory",
                UnitMovement =
                {
                    Walking = 5,
                    Running = 5
                },
                Components = GetBasicBipedMechLocations(),
                Equipment = GetBasicStandardEquipmentLoadout()
            };

        public static TrackedGameElement BuildTrackedGameElement(BattleMech template)
        {
            return new TrackedGameElement(BuildMechFromTemplate(template));
        }

        public static TrackedGameElement BuildTrackedGameElement(IndustrialMech template)
        {
            return new TrackedGameElement(BuildMechFromTemplate(template));
        }

        private static BattleMech BuildMechFromTemplate(BattleMech template) 
        {
            var mech = new BattleMech
            {
                Name = template.Name,
                Tonnage = template.Tonnage,
                Model = template.Model,
                TechBase = template.TechBase,
                RulesLevel = template.RulesLevel,
                BattleValue = template.BattleValue,
                HeatSinks = template.HeatSinks,
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

            foreach(var component in mech.Components) component.SetOriginalValuesFromCurrentValues();

            return mech;
        }

        private static IndustrialMech BuildMechFromTemplate(IndustrialMech template)
        {
            var mech = new IndustrialMech()
            {
                Name = template.Name,
                Tonnage = template.Tonnage,
                Model = template.Model,
                TechBase = template.TechBase,
                RulesLevel = template.RulesLevel,
                BattleValue = template.BattleValue,
                HeatSinks = template.HeatSinks,
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

            return mech;
        }


        private static IEnumerable<UnitComponent> GetBasicBipedMechLocations()
        {
            return new List<UnitComponent>
            {
                new UnitComponent(){Name = "Head"},
                new UnitComponent(){Name = "Center Torso"},
                new UnitComponent(){Name = "Left Torso"},
                new UnitComponent(){Name = "Right Torso"},
                new UnitComponent(){Name = "Right Arm"},
                new UnitComponent(){Name = "Left Arm"},
                new UnitComponent(){Name = "Right Leg"},
                new UnitComponent(){Name = "Left Leg"}
            };
        }

        private static IEnumerable<Equipment> GetBasicStandardEquipmentLoadout()
        {
            return new List<Equipment>
            {
                new Equipment(){Name = "Shoulder", Hits=1, Location="LA"},
                new Equipment(){Name = "Shoulder", Hits=1, Location="RA"},
                new Equipment(){Name = "Upper Arm Actuator", Hits=1, Location="LA"},
                new Equipment(){Name = "Upper Arm Actuator", Hits=1, Location="RA"},
                new Equipment(){Name = "Lower Arm Actuator", Hits=1, Location="LA"},
                new Equipment(){Name = "Lower Arm Actuator", Hits=1, Location="RA"},
                new Equipment(){Name = "Hand Actuator", Hits=1, Location="LA"},
                new Equipment(){Name = "Hand Actuator", Hits=1, Location="RA"},
                new Equipment(){Name = "Life Support", Hits=1, Location="H"},
                new Equipment(){Name = "Sensors", Hits=2, Location="H"},
                new Equipment(){Name = "Cockpit", Hits=1, Location="H"},
                new Equipment(){Name = "Fusion Engine", Hits=3, Location="CT"},
                new Equipment(){Name = "Gyro", Hits=2, Location="CT"},
                new Equipment(){Name = "Heat Sink", Hits=1, Location="RT"},
                new Equipment(){Name = "Heat Sink", Hits=1, Location="LT"},
                new Equipment(){Name = "Hip", Hits=1, Location="LL"},
                new Equipment(){Name = "Hip", Hits=1, Location="RL"},
                new Equipment(){Name = "Upper Leg Actuator", Hits=1, Location="LL"},
                new Equipment(){Name = "Upper Leg Actuator", Hits=1, Location="RL"},
                new Equipment(){Name = "Lower Leg Actuator", Hits=1, Location="LL"},
                new Equipment(){Name = "Lower Leg Actuator", Hits=1, Location="RL"},
                new Equipment(){Name = "Foot Actuator", Hits=1, Location="LL"},
                new Equipment(){Name = "Foot Actuator", Hits=1, Location="RL"}
            };
        }
    }
}
