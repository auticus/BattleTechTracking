using System;
using System.Windows.Input;
using BattleTechTracking.Models;
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
                var loadView = new LoadMatchStateView();
                if (!(loadView.BindingContext is LoadFileViewModel loadViewModel))
                {
                    throw new ArgumentNullException("LoadFileView returns object that is not expected");
                }

                loadViewModel.SaveFileIsVisible = true;
                loadViewModel.OnFileViewModelLoaded += OnFileViewModelLoaded;
                await PageNavigation.PushAsync(loadView);
            });
        }

        private async void OnFileViewModelLoaded(object sender, MatchState data)
        {
            await PageNavigation.PopAsync();
            if (data == MatchState.NoMatchStateLoaded) return;

            await PageNavigation.PushAsync(new MatchView(data));
        }
    }
}
