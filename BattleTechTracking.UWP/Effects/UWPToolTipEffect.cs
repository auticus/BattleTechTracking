using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BattleTechTracking.Effects;
using BattleTechTracking.UWP.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("AuticusStudios")]
[assembly: ExportEffect(typeof(UWPToolTipEffect), nameof(TooltipEffect))]

namespace BattleTechTracking.UWP.Effects
{
    public class UWPToolTipEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            //every xaml element that calls the TooltipEffect in the shared assembly will have this fire for that element.

            var control = Control ?? Container;

            if (control is DependencyObject)
            {
                var toolTip = new ToolTip
                {
                    Content = TooltipEffect.GetText(Element)
                };

                switch (TooltipEffect.GetPosition(Element))
                {
                    case TooltipPosition.Bottom:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Bottom;
                        break;
                    case TooltipPosition.Top:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Top;
                        break;
                    case TooltipPosition.Left:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Left;
                        break;
                    case TooltipPosition.Right:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Right;
                        break;
                    default:
                        return;
                }
                ToolTipService.SetToolTip(control, toolTip);
            }
        }

        protected override void OnDetached()
        {
            
        }
    }
}
