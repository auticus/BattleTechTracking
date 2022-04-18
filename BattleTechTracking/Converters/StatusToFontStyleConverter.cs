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
                case UnitStatusFactory.UNDAMAGED:
                case UnitStatusFactory.DAMAGED:
                case UnitStatusFactory.CRIPPLED:
                    return FontAttributes.Bold;
                case UnitStatusFactory.DESTROYED:
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
