using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BattleTechTracking.Models;
using BattleTechTracking.Utilities;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public abstract class FileViewModelBase : BaseViewModel
    {
        private bool _saveFileIsVisible;
        private string _saveFileName;
        private List<string> _fileNames;

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

        public ICommand SaveFileOkButtonPressed { get; protected set; }
        public ICommand SaveFileCancelButtonPressed { get; protected set; }
        public ICommand DeleteFileButtonPressed { get; }

        protected FileViewModelBase()
        {
            DeleteFileButtonPressed = new Command<string>((fileName) =>
            {
                DataPump.DeleteMatchState(fileName);
                LoadFileNamesFromSaveLocation();
            });
        }
        
        private void LoadFileNamesFromSaveLocation()
        {
            FileNames = DataPump.GetAllSavedGameFileNames().ToList();
        }
    }
}
