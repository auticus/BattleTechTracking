using System.Collections.ObjectModel;
using System.Windows.Input;
using BattleTechTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BattleTechTracking.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuirksView : ContentView
    {
        public static readonly BindableProperty QuirkSourceProperty = BindableProperty.Create(
            nameof(QuirkSource),
            typeof(ObservableCollection<Quirk>),
            typeof(QuirksView));

        public static readonly BindableProperty SelectedQuirkProperty = BindableProperty.Create(
            nameof(SelectedQuirk),
            typeof(string),
            typeof(QuirksView));

        public static readonly BindableProperty NewQuirkProperty = BindableProperty.Create(
            nameof(NewQuirk),
            typeof(ICommand),
            typeof(QuirksView));

        public static readonly BindableProperty DeleteQuirkProperty = BindableProperty.Create(
            nameof(DeleteQuirk),
            typeof(ICommand),
            typeof(QuirksView));

        public QuirksView()
        {
            InitializeComponent();
        }

        public ObservableCollection<Quirk> QuirkSource
        {
            get => (ObservableCollection<Quirk>)GetValue(QuirksView.QuirkSourceProperty);
            set => SetValue(QuirksView.QuirkSourceProperty, value);
        }

        public string SelectedQuirk
        {
            get => (string)GetValue(QuirksView.SelectedQuirkProperty);
            set => SetValue(QuirksView.SelectedQuirkProperty, value);
        }

        public Command NewQuirk
        {
            get => (Command)GetValue(QuirksView.NewQuirkProperty);
            set => SetValue(QuirksView.NewQuirkProperty, value);
        }

        public Command DeleteQuirk
        {
            get => (Command)GetValue(QuirksView.DeleteQuirkProperty);
            set => SetValue(QuirksView.DeleteQuirkProperty, value);
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