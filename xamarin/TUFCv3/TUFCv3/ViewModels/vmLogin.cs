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
        public User user { set; get; }                                          // Login user
        public Navigation navigation = new Navigation();                        // Navigates to another page


        // Constructor
        public vmLogin()
        {
            user = new User();          // Instantiate the object 'user' (which is bound to Login.xaml entry fields)
            DefineNavigationCommands();       // Create navigation commands for button click events 
        }


        // Instantiate Commands
        public ICommand cmdLogin { private set; get; }          // Triggered when the user presses the 'Login' button
        public ICommand cmdNavigation { private set; get; }     // Triggered when the user presses the 'New User' button


        // Command definitions
        void DefineNavigationCommands()
        {
            // cmdNavigation()
            /*  Generic navigation call
                - used to navigate to the page NewUser */
            cmdNavigation = new Command<Type>(
                execute: async (Type selectedPage) =>
                    await navigation.GoToPage(selectedPage));


            // cmdLogin()
            /*  When the 'Login' button is pressed
                authenticate the user, then navigate to the page 'MainMenu' */     
            cmdLogin = new Command<Type>(
                execute: async (Type selectedPage) =>
                {
                    AuthenticateUser authenticateUser = new AuthenticateUser();         // Create AuthenticateUser object 
                    bool authenticated = await authenticateUser.Authenticate(user);     // Check password

                    if (authenticated)                                                  // If the password is okay:
                    {
                        await navigation.GoToPage(selectedPage);                        //  - go to MainMenu
                    }
                    else
                    {                                                                   // If password doesn't match:
                        await App.Current.MainPage.DisplayAlert                         //   - display the authentication error errorMessage.
                        ("Login error", authenticateUser.errorMessage, "Okay");
                    }
                });
        }
    }
}
