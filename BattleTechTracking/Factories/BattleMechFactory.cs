using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public static class BattleMechFactory
    {
        public static BattleUnit BuildDefaultBattleMech()
        {
            var mech = new BattleUnit()
            {
                Name = "Unnamed",
                Tonnage = 20,
                Model = "Unknown",
                TechBase = "Inner Sphere",
                RulesLevel = "Introductory"
            };

            mech.UnitMovement.Walking = 5;
            mech.UnitMovement.Running = 5;

            mech.Components = GetBasicBipedMechLocations();
            mech.Equipment = GetBasicStandardEquipmentLoadout();
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
