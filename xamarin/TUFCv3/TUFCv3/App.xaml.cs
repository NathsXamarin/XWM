using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace TUFCv3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());      // Allow page nagiation in the Xamarin app
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
