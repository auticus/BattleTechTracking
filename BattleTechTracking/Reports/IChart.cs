using Xamarin.Forms;

namespace BattleTechTracking.Reports
{
    public interface IChart
    {
        /// <summary>
        /// Xaml to generate a grid on the user interface.
        /// </summary>
        /// <returns></returns>
        Grid GenerateChart();
    }
}
