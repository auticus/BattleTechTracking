using System.Collections.ObjectModel;
using BattleTechTracking.Models;

namespace BattleTechTracking.Utilities
{
    /// <summary>
    /// Interface for elements that can participate in physical combat
    /// </summary>
    public interface IPhysicalCombatant
    {
        IDisplayListView GameElement { get; }
        int PilotPilotingSkill { get; }
        ObservableCollection<Equipment> UnitEquipment { get; }
    }
}
