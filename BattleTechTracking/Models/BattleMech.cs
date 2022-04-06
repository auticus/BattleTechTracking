using System.Collections.Generic;

namespace BattleTechTracking.Models
{
    public class BattleMech : BaseUnit
    {
        public IEnumerable<UnitComponent> BuildDefaultLocations()
        => new List<UnitComponent>
            {
                new UnitComponent(){Name = "Head"},
                new UnitComponent(){Name = "Center Torso"},
                new UnitComponent(){Name = "Right Torso"},
                new UnitComponent(){Name = "Left Torso"},
                new UnitComponent(){Name = "Rear Right Torso"},
                new UnitComponent(){Name = "Rear Left Torso"},
                new UnitComponent(){Name = "Right Arm"},
                new UnitComponent(){Name = "Left Arm"},
                new UnitComponent(){Name = "Right Leg"},
                new UnitComponent(){Name = "Left Leg"}
            };
        
    }
}
