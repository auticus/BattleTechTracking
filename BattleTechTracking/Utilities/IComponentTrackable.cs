using System.Collections.ObjectModel;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    public interface IComponentTrackable
    {
        ObservableCollection<UnitComponent> UnitComponents { get; } 
        ObservableCollection<Equipment> UnitEquipment { get; }
        ObservableCollection<Weapon> UnitWeapons { get; }
        ObservableCollection<Ammunition> UnitAmmunition { get; }
    }
}
