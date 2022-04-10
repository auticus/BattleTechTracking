using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Views
{
    /// <summary>
    /// Represents the view that handles data import into the application.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataView : ContentPage
    {
        
        public DataView()
        {
            InitializeComponent();
            
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            var textBox = sender as Entry;
            if (textBox?.Text == null) return;

            textBox.CursorPosition = 0;
            textBox.SelectionLength = textBox.Text.Length;
        }
    }
}