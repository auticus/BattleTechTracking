using System;
using System.Windows.Input;
using BattleTechTracking.Factories;
using BattleTechTracking.Utilities;
using BattleTechTracking.Views;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand OpenDataView { get; }
        public ICommand CreateNewGame { get; }
        public ICommand LoadExistingGame { get; }
        public ICommand ViewSavedGame { get; }


        public MainPageViewModel()
        {
            OpenDataView = new Command(async () =>
            {
                await PageNavigation.PushAsync(new DataView());
            });

            CreateNewGame = new Command(async () =>
            {
                await PageNavigation.PushAsync(new MatchView());
            });
            LoadExistingGame = new Command(async () =>
            {
                var matchState = DataPump.LoadMatchState("SavedMatchState.json");
                await PageNavigation.PushAsync(new MatchView(matchState));
            });

            ViewSavedGame = new Command(() => throw new NotImplementedException());
        }
    }
}
