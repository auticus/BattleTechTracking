using System;
using System.Globalization;
using BattleTechTracking.Factories;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class StatusToFontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case EquipmentStatus.UNDAMAGED:
                case EquipmentStatus.DAMAGED:
                case EquipmentStatus.CRIPPLED:
                    return FontAttributes.Bold;
                case EquipmentStatus.DESTROYED:
                    return FontAttributes.None;
            }

            return Color.Default;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
