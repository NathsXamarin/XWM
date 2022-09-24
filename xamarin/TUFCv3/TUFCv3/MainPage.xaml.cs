using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using MySqlConnector;
using System.Threading;
using Xamarin.Forms;

namespace TUFCv3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigateToLoginPage();
            // NavigateToMySqlConn();       // Comment out, but DO NOT DELETE (Only used during database connectivity diagnostics)
        }


        async void NavigateToLoginPage()
        {
            await Navigation.PushModalAsync(new Views.Login());        // Navigate to MySqlConn.xaml 
        }


        async void NavigateToMySqlConn()
        {                                                  
            await Navigation.PushModalAsync(new Additional.Archive.MySqlConn());        // Navigate to MySqlConn.xaml 
        }
    }
}

