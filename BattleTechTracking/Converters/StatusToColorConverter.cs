using System;
using System.Globalization;
using BattleTechTracking.Factories;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case EquipmentStatus.UNDAMAGED:
                    return Color.Default;
                case EquipmentStatus.DAMAGED:
                    return Color.Olive;
                case EquipmentStatus.CRIPPLED:
                    return Color.DarkRed;
                case EquipmentStatus.DESTROYED:
                    return Color.FromHex("#202421");
            }

            return Color.Default;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}