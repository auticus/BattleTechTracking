using System;
using System.Globalization;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    internal class EquipmentLocationToFontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == EquipmentStatus.DESTROYED ? FontAttributes.None : FontAttributes.Bold;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
