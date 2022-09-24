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

namespace TUFCv3.ViewModels 
{
    public class vmLogin
    {
        // Properties
        public User user { set; get; }
        public AuthenticateUser authenticateUser = new AuthenticateUser();


        // Constructor
        public vmLogin()
        {         
            user = new User();                      // Instantiate the object 'user' (which is bound to 'Login' entry fields)
            authenticateUser.loginUser = user;      // Set the object 'user' in authenticateUser to the same as this class's 'user' object

            DefineCommands();                       // Create commands for button events, triggered from the View 'Login.xaml' 
        }


        // Instantiate Commands
        public ICommand cmdLogin { private set; get; }          // Triggered when the user presses the 'Login' button
        public ICommand cmdNavigation { private set; get; }     // Triggered when the user presses the 'New User' button


        // Command definitions
        void DefineCommands()
        {
            // cmdLogin
            // When the 'Login' button is pressed
            // authenticate the user, then navigate to the page 'MainMenu'
            cmdLogin = new Command<Type>(
                execute: async (Type MainMenu) =>
                {         
                    authenticateUser.AuthenticationSequence(user);                      // Authenticate the user password with the database

                    if (authenticateUser.result)                                    // If the passwords match:
                    {
                        Page page = (Page)Activator.CreateInstance(MainMenu);           //  create the page MainMenu()
                        await App.Current.MainPage.Navigation.PushModalAsync(page);     //  and navigate to it.
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert                         // Otherwise, display the authentication error message.
                        ("Login error", authenticateUser.message, "Okay");          
                    }
                });


            // cmdNavigation
            // This is a generic navigation command, triggered by a button press
            //  which does not require authentication (eg to the view NewUser.xaml)
            cmdNavigation = new Command<Type>(
                execute: async (Type selectedPage) =>
                {
                    Page page = (Page)Activator.CreateInstance(selectedPage);       // Create the page
                    await App.Current.MainPage.Navigation.PushModalAsync(page);     //  and navigate to it.
                });
        }
    }
}
