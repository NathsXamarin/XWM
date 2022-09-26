using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Security;
using TUFCv3.Views;
using Xamarin.Forms;
using System;
using System.Windows.Input;
using TUFCv3.Models;
using TUFCv3.Additional;
using System.Threading.Tasks;

namespace TUFCv3.ViewModels 
{
    public class vmLogin
    {
        // Properties
        public User user { set; get; }
        public AuthenticateUser authenticateUser = new AuthenticateUser();
        public Navigation navigation = new Navigation();


        // Constructor
        public vmLogin()
        {
            user = new User();          // Instantiate the object 'user' (which is bound to Login.xaml entry fields)
            NavigationCommands();       // Create navigation commands for button click events 
        }


        // Instantiate Commands
        public ICommand cmdLogin { private set; get; }          // Triggered when the user presses the 'Login' button
        public ICommand cmdNavigation { private set; get; }     // Triggered when the user presses the 'New User' button


        // Command definitions
        void NavigationCommands()
        {
            // cmdNavigation
            cmdNavigation = new Command<Type>(
                execute: async (Type selectedPage) =>
                    await navigation.GoToPage(selectedPage));


            // cmdLogin
            // When the 'Login' button is pressed
            // authenticate the user, then navigate to the page 'MainMenu'
            cmdLogin = new Command<Type>(
                execute: async (Type selectedPage) =>
                {
                    if (await authenticateUser.Authenticate(user))                                    // If the passwords match:
                    {
                        Page page = (Page)Activator.CreateInstance(selectedPage);           //  create the page MainMenu()
                        await App.Current.MainPage.Navigation.PushModalAsync(page);     //  and navigate to it.
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert                         // Otherwise, display the authentication error errorMessage.
                        ("Login error", authenticateUser.errorMessage, "Okay");
                    }
                });
        }
    }
}
