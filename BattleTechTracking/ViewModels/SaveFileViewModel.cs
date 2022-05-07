using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Models;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public class SaveFileViewModel : BaseViewModel
    {
        private bool _saveFileIsVisible;
        private string _saveFileName;
        private List<string> _fileNames;

        /// <summary>
        /// Event that fires when the <see cref="SaveFileViewModel"/> is finished and can lose focus.
        /// </summary>
        public EventHandler OnFileViewModelFinished { get; set; }

        /// <summary>
        /// Gets or sets the match state that is saved to file.
        /// </summary>
        public MatchState SavedMatchState { get; set; }

        public string SaveFileName
        {
            get => _saveFileName;
            set
            {
                _saveFileName = value;
                OnPropertyChanged(nameof(SaveFileName));
            }
        }

        public bool SaveFileIsVisible
        {
            get => _saveFileIsVisible;
            set
            {
                if (value) LoadFileNamesFromSaveLocation();
                _saveFileIsVisible = value;
                OnPropertyChanged(nameof(SaveFileIsVisible));
            }
        }

        public List<string> FileNames
        {
            get => _fileNames;
            set
            {
                _fileNames = value;
                OnPropertyChanged(nameof(FileNames));
            }
        }

        public ICommand SaveFileOkButtonPressed { get; }
        public ICommand SaveFileCancelButtonPressed { get; }

        public SaveFileViewModel()
        {
            SaveFileCancelButtonPressed = new Command(() =>
            {
                OnFileViewModelFinished?.Invoke(this, EventArgs.Empty);
            });

            SaveFileOkButtonPressed = new Command(() =>
            {
                if (!string.IsNullOrEmpty(SaveFileName)) DataPump.SaveMatchState(SavedMatchState, SaveFileName);
                SaveFileName = string.Empty;
                OnFileViewModelFinished?.Invoke(this, EventArgs.Empty);
            });
        }

        private void LoadFileNamesFromSaveLocation()
        {
            FileNames = DataPump.GetAllSavedGameFileNames().ToList();
        }
    }
}
