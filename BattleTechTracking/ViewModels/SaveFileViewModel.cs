using System;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class SaveFileViewModel : FileViewModelBase
    {
        /// <summary>
        /// Event that fires when the <see cref="SaveFileViewModel"/> has saved its data.
        /// </summary>
        public EventHandler OnFileViewModelSaved { get; set; }

        public SaveFileViewModel()
        {
            SaveFileCancelButtonPressed = new Command(() =>
            {
                OnFileViewModelSaved?.Invoke(this, EventArgs.Empty);
            });

            SaveFileOkButtonPressed = new Command(() =>
            {
                if (!string.IsNullOrEmpty(SaveFileName)) DataPump.SaveMatchState(SavedMatchState, SaveFileName);
                SaveFileName = string.Empty;
                OnFileViewModelSaved?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
