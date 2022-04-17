using System;
using System.Globalization;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    public class SelectedBoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((bool)value) ? Color.Gold : Color.WhiteSmoke;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (Color)value == Color.Gold;
        
    }
}
