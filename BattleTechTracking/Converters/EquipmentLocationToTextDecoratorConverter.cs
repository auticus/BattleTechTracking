using System;
using System.Globalization;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    internal class EquipmentLocationToTextDecoratorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == EquipmentStatus.DESTROYED ? TextDecorations.Strikethrough : TextDecorations.None;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
