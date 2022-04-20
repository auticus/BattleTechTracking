using System;
using System.Globalization;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class EquipmentLocationToColorStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == EquipmentStatus.DESTROYED ? Color.FromHex("#202421") : Color.Default;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
