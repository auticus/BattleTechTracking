using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchView : ContentPage
    {
        public MatchView()
        {
            InitializeComponent();
        }

        public MatchView(MatchState state) : this()
        {
            viewModel.LoadMatchState(state);
        }
    }
}