using System;
using System.Windows.Input;
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

            CreateNewGame = new Command(() => throw new NotImplementedException());
            LoadExistingGame = new Command(() => throw new NotImplementedException());
            ViewSavedGame = new Command(() => throw new NotImplementedException());
        }
    }
}
