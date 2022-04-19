using System;
using System.Globalization;
using BattleTechTracking.Models;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class ComponentStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (UnitComponentStatus)value;

            switch (status)
            {
                case UnitComponentStatus.Undamaged:
                    return Color.Default;
                case UnitComponentStatus.LightlyDamage:
                    return Color.Olive;
                case UnitComponentStatus.ModeratelyDamaged:
                    return Color.OrangeRed;
                case UnitComponentStatus.StructuralDamage:
                    return Color.DarkRed;
                case UnitComponentStatus.Destroyed:
                    return Color.DimGray;
                default:
                    throw new ArgumentException(
                        "Enum value UnitComponentStatus contains values not developed for converter");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
