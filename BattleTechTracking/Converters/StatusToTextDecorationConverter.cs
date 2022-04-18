using System;
using System.Globalization;
using BattleTechTracking.Factories;
using Xamarin.Forms;

namespace BattleTechTracking.Converters
{
    internal class StatusToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case UnitStatusFactory.UNDAMAGED:
                case UnitStatusFactory.DAMAGED:
                case UnitStatusFactory.CRIPPLED:
                    return TextDecorations.None;
                case UnitStatusFactory.DESTROYED:
                    return TextDecorations.Strikethrough;
            }

            return Color.Default;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
