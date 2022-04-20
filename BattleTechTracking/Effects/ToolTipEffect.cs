/*
 * This class was pulled from https://www.xamboy.com/2019/03/01/showing-tooltips-in-xamarin-forms/
 * It highlights the layers required to get platform-specific functionality.  In this case, effects.
 * The base effect is created here in the shared project and can be referenced, but the implementation happens
 * in the platform specific code.
 */

using System.Linq;
using Xamarin.Forms;

namespace BattleTechTracking.Effects
{
    public enum TooltipPosition
    {
        Top,
        Left,
        Right,
        Bottom
    }

    public static class TooltipEffect
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.CreateAttached("Text", typeof(string), typeof(TooltipEffect), string.Empty, propertyChanged: OnTextChanged);

        public static readonly BindableProperty PositionProperty =
            BindableProperty.CreateAttached("Position", typeof(TooltipPosition), typeof(TooltipEffect), TooltipPosition.Bottom);


        public static string GetText(BindableObject view)
        {
            return (string)view.GetValue(TextProperty);
        }

        public static void SetText(BindableObject view, string value)
        {
            view.SetValue(TextProperty, value);
        }

        public static TooltipPosition GetPosition(BindableObject view)
        {
            return (TooltipPosition)view.GetValue(PositionProperty);
        }

        public static void SetPosition(BindableObject view, TooltipPosition value)
        {
            view.SetValue(PositionProperty, value);
        }

        static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // when the xaml elements bind in the shared assembly, it will fire into the UWP solution to pull the effect.  That effect has code
            // that will change the text here, which is wired to fire OnTextChanged.

            var view = bindable as View;
            if (view == null)
            {
                return;
            }

            string text = (string)newValue;
            if (!string.IsNullOrEmpty(text))
            {
                view.Effects.Add(new ControlTooltipEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is ControlTooltipEffect);
                if (toRemove != null)
                {
                    view.Effects.Remove(toRemove);
                }
            }
        }
    }

    public class ControlTooltipEffect : RoutingEffect
    {
        public ControlTooltipEffect() : base("AuticusStudios.TooltipEffect")
        {

        }
    }
}
