using Xamarin.Forms;

[assembly: ExportFont("Hvymtl1.ttf", Alias="BattletechFont")]
namespace BattleTechTracking
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
