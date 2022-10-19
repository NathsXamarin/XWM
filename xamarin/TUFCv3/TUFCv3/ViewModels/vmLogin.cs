using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Security;
using TUFCv3.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Windows.Input;
using TUFCv3.Additional;
using TUFCv3.Models.Users;
using TUFCv3.Additional.Navigation;

namespace TUFCv3.ViewModels
{
    public class vmLogin
    {
        // Properties
        public IUser loginUser { set; get; }            // Login loginUser          
        public INavigate navigate = new Navigate();     // Navigates to another page


        // Constructor
        public vmLogin()
        {
            loginUser = new User();              // Instantiate the object 'loginUser' (which is bound to Login.xaml entry fields)
            DefineNavigationCommands();     // Create navigate commands for button click events 
        }


        // Instantiate Commands
        public ICommand cmdLogin { private set; get; }          // Triggered when the loginUser presses the 'Login' button
        public ICommand cmdNavigation { private set; get; }     // Triggered when the loginUser presses the 'New User' button


        // Command definitions
        void DefineNavigationCommands()
        {
            // cmdNavigation()
            /*  Generic navigate call
                - used to navigate to the page NewUser */
            cmdNavigation = new Command<Type>(
                execute: async (Type selectedPage) =>
                    await navigate.GoToPage(selectedPage));


            // cmdLogin()
            /*  When the 'Login' button is pressed
                authenticate the loginUser, then navigate to the page 'MainMenu' */     
            cmdLogin = new Command<Type>(
                execute: async (Type selectedPage) =>
                {
                    AuthenticateUser authenticateUser = new AuthenticateUser();         // Create AuthenticateUser object 
                    bool authenticated = await authenticateUser.Authenticate(loginUser);     // Check password

                    if (authenticated)                                                  // If the password is okay:
                    {
                        await navigate.GoToPage(selectedPage);                        //  - go to MainMenu
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
