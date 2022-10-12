using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Data.Common;
using MySqlConnector;
using System;
using TUFCv3.Additional.MySql;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using TUFCv3.Models;
using TUFCv3.Additional.Encryption;

namespace TUFCv3.Additional
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class AuthenticateUser
    {
        private User databaseUser = new User();     // User details from the database (compared to loginUser to authenticate) 
        public string errorMessage;                 // Authentication errorMessage


        /*  Authenticate()         
            Call methods which:
                - get user details from the database
                - compares the user's login password with the one saved on the database
                - if authenication is successful, return True.  */
        public async Task<bool> Authenticate(User loginUser)
        {
            if (   !GetDatabaseUser(loginUser)          // If user details can not be retrieved from the database
                || !ComparePasswords(loginUser))        //  or the login password is incorrect
                return false;                           //  return False.
            else
            {                                           // If the user's login password authenticates
                return true;                            //  return True. 
            }
        }


        /*  GetDatabaseUser()
            Call the method 'RunQuery'
            to get user details from the database  */
        bool GetDatabaseUser(User loginUser)
        {
            MySql.GetLoginDetails getUser = new MySql.GetLoginDetails();        
            databaseUser = getUser.RunQuery(loginUser);      // Get user object from the database.

            if(databaseUser == null)                            // If the user can not be found: 
            {
                errorMessage = getUser.errorMessage;            //  - create error message
                return false;                                   //  - and return False.
            }
            return true;                                        // If user data is okay, return True.
        }



        /*  ComparePasswords()
            Compare the login password 
            to the encrypted password retrieved from the database
            by encrypting the login password using the same algorithm  */
        bool ComparePasswords(User loginUser)
        {
            EncryptionCbc encryption = new EncryptionCbc();                                       
            string encryptedLoginPassword = encryption.EncryptText(loginUser.Password);   // Encrypt the user's login password.

            if (encryptedLoginPassword != databaseUser.Password)                            // If the login and database passwords do not match:
            {                                                                               
                errorMessage = "Incorrect password";                                        //   - create error message
                return false;                                                               //   - return False.
            }
            else
                return true;                                                                // If the passwords match, return True.
        }
    } 
}
