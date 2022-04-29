using System.Collections.Generic;
using System.Collections.ObjectModel;
using BattleTechTracking.Models;

namespace BattleTechTracking.Reports
{
    /// <summary>
    /// Interface used for elements that can have Reports run on them.
    /// </summary>
    public interface IReportable
    {
        string UnitHeader { get; }
        ObservableCollection<UnitComponent> UnitComponents { get; }
        ObservableCollection<Equipment> UnitEquipment { get; }
        ObservableCollection<Weapon> UnitWeapons { get; }
        ObservableCollection<Ammunition> UnitAmmunition { get; }
    }
}
