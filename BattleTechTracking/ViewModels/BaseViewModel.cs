using System.ComponentModel;
using Xamarin.Forms;

namespace BattleTechTracking.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private INavigation _nav;

        /// <summary>
        /// Reference to the Main Page's navigation object.
        /// </summary>
        protected INavigation PageNavigation =>
            // navigation object can normally be injected into a constructor.  HOWEVER -> if binding to a view model from xaml
            // you don't have the ability to pass things in to a constructor (its an empty constructor like this)
            // to get around that wec an use the Application object to get the root page nav.
            _nav ?? (_nav = Application.Current.MainPage.Navigation);

        protected virtual void OnProperyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
