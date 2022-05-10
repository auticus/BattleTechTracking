using System;
using BattleTechTracking.Models;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class LoadFileViewModel : FileViewModelBase
    {
        /// <summary>
        /// Event that fires when the <see cref="SaveFileViewModel"/> has saved its data.
        /// </summary>
        public EventHandler<MatchState> OnFileViewModelLoaded { get; set; }

        public LoadFileViewModel()
        {
            SaveFileCancelButtonPressed = new Command(() =>
            {
                OnFileViewModelLoaded?.Invoke(this, MatchState.NoMatchStateLoaded);
            });

            SaveFileOkButtonPressed = new Command(() =>
            {
                var saveState = string.IsNullOrEmpty(SaveFileName)
                    ? MatchState.NoMatchStateLoaded
                    : DataPump.LoadMatchState(SaveFileName);
                SaveFileName = string.Empty;
                OnFileViewModelLoaded?.Invoke(this, saveState);
            });
        }
    }
}
