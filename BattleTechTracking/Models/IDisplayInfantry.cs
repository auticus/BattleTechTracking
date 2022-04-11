namespace BattleTechTracking.Models
{
    public interface IDisplayInfantry
    {
        string Name { get; set; }
        string Weapon { get; set; }
        string Range { get; set; }
        string ArmorType { get; set; }
        int Number { get; set; }
        double Tonnage { get; set; }
        int Movement { get; set; }
        int BattleValue { get; set; }
    }
}
