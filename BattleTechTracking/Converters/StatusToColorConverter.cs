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
                case UnitStatusFactory.UNDAMAGED:
                    return Color.Default;
                case UnitStatusFactory.DAMAGED:
                    return Color.Olive;
                case UnitStatusFactory.CRIPPLED:
                    return Color.DarkRed;
                case UnitStatusFactory.DESTROYED:
                    return Color.FromHex("#343b35");
            }

            return Color.Default;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}