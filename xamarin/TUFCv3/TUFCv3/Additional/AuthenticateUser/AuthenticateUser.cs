using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Data.Common;
using MySqlConnector;
using System;
using TUFCv3.Models;
using TUFCv3.Additional.MySql;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using TUFCv3.Additional.Encryption;
using TUFCv3.Models.Users;

namespace TUFCv3.Additional.AuthenticateUser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class AuthenticateUser : IAuthenticateUser
    {
        public IUser databaseUser { get; set; }         // User details from the database (compared to loginUser to authenticate) 
        public string errorMessage { get; set; }        // Authentication errorMessage


        /*  Constructor */
        public AuthenticateUser()
        {
            databaseUser = Factory.CreateUser();        // Instantiate the user databaseUser
        }


        /*  Authenticate()         
            Call methods which:
                - get loginUser details from the database
                - compares the loginUser's login password with the one saved on the database
                - if authenication is successful, return True.  */
        public async Task<bool> Authenticate(IUser loginUser)
        {
            if (   !GetDatabaseUser(loginUser)          // If loginUser details can not be retrieved from the database
                || !ComparePasswords(loginUser))        //  or the login password is incorrect
                return false;                           //  return False.
            else
            {                                           // If the loginUser's login password authenticates
                return true;                            //  return True. 
            }
        }


        /*  GetDatabaseUser()
            Call the method 'RunQuery'
            to get loginUser details from the database  */
        public bool GetDatabaseUser(IUser loginUser)
        {
            IGetMySqlData getLoginDetails = Factory.CreateGetLoginDetails();
            databaseUser = getLoginDetails.RunQuery(loginUser);     // Get loginUser object from the database.

            if (databaseUser == null)                               // If the loginUser can not be found: 
            {
                errorMessage = getLoginDetails.errorMessage;        //  - create error message
                return false;                                       //  - and return False.
            }
            return true;                                            // If loginUser data is okay, return True.
        }



        /*  ComparePasswords()
            Compare the login password 
            to the encrypted password retrieved from the database
            by encrypting the login password using the same algorithm  */
        public bool ComparePasswords(IUser loginUser)
        {
            IEncryptionCbcCombo encryption = Factory.CreateEncryptionCbcCombo();
            string encryptedLoginPassword = encryption.EncryptText(loginUser.Password);     // Encrypt the loginUser's login password.

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
