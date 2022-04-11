using System.Collections.Generic;
using BattleTechTracking.Models;

namespace BattleTechTracking.Factories
{
    public class CombatVehicleFactory
    {
        public static CombatVehicle BuildDefaultCombatVehicle()
        {
            var vehicle = new CombatVehicle()
            {
                Name = "Unnamed",
                Tonnage = 10,
                Model = "Unknown",
                TechBase = "Inner Sphere",
                RulesLevel = "Introductory"
            };

            vehicle.UnitMovement.Walking = 5;
            vehicle.UnitMovement.Running = 5;

            vehicle.Components = GetBasicVehicleLocations();
            vehicle.Equipment = GetBasicStandardEquipmentLoadout();
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
