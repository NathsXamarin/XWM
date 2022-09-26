using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Data.Common;
using Xamarin.Forms.Xaml;
using MySqlConnector;
using System;
using TUFCv3.Models;

namespace TUFCv3.Additional
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class AuthenticateUser
    {
        MySqlConnection connection = new MySqlConnection();     // MySQL connection

        public User loginUser = new User();                     // User login details from the View 'Login' 
        public User databaseUser = new User();                  // User details from the database (compared to loginUser to authenticate)                                                                

        public string errorMessage;                             // Authentication errorMessage


        public async Task<bool> Authenticate(User _user)
        {
            loginUser = _user;                  // User login details from the View 'Login'  

            // Call methods to authenticate the user
            if (   !ConnectToMysql()
                || !GetUserData()
                || !ComparePasswords())
                return false;                   //  If any the above methods fail, return false
            else
            {                 
                return true;                    // Otherwise return true 
            }
        }


        public bool ConnectToMysql()
        {
            // Create a MySqlConnection, using connection details for the server 'xwm-mysql' 
            connection = new MySqlConnection(
                "Server=xwm-mysql; " +
                "Database=tufc; " +
                "User ID=admin; " +
                "Password=adm1n; "
                );
            try
            {
                connection.Open();              // Test the database connection by opeing it.              
                connection.Close();             //  then close the connection 
                return true;
            }
            catch(Exception ex)                 // If the connection fails
            {
                errorMessage = ex.Message;       //  and set the error errorMessage
                return false;
            }
        }


        bool GetUserData()
        {
            // Connect to the server
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                errorMessage =  ex.Message;      //  and set the error errorMessage
                return false;
            }

            // Create the SELECT sqlCmd, that will be sent to the database,
            //  returning the user's email address, password and createDate
            string sqlCmd =
                "SELECT email, password, createDate, country " +
                "FROM User " +
                "WHERE email = @email ";

            using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))             // Create a MySqlCommand
            {
                command.Parameters.Add(new MySqlParameter("@email", loginUser.Email));      //  and include the users email address

                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())    // Send the sqlCmd to the MySQL database
                    {
                        if(!reader.HasRows)                                     // If no data is returned for the email address            
                        {           
                            errorMessage = "Email does not exist";                   //  and set the error errorMessage
                            return false;                                       //  and return
                        }

                        if (reader.Read())                                              // If the user id found, add the returned data to databaseUser properties
                        {
                            databaseUser.Email      = reader.GetValue(0).ToString();    // There is a method GetString(), but it causes errors  
                            databaseUser.Password   = reader.GetValue(1).ToString();    //  when returning null values from the database.
                            databaseUser.CreateDate = reader.GetValue(2).ToString();    // It's better to use GetValue() and ToString()
                            databaseUser.Country    = reader.GetValue(3).ToString();    //  that way, if a database field is null, it is converted it to ""    
                        }
                    }
                }
                catch(Exception ex)                 // If getting a valid row from the database does not work
                {
                    errorMessage = ex.Message;       //  and set the error errorMessage
                    return false;
                }
            }
            return true;
        }



        // ComparePasswords()
        // Encrypt the password the user entered on the page 'Login'
        //  and compare it to the encrypted password received from the database. 
        bool ComparePasswords()
        {
            Encryption encryption = new Encryption();                                           // Create the encrypting object
            string encryptedLoginPassword = encryption.EncryptString(loginUser.Password);       // Encrypt the password the user entered

            if (encryptedLoginPassword != databaseUser.Password)        // If the user and database passwords don't match
            {
                errorMessage = "Incorrect password";                     //  and set the error errorMessage
                return false;                                             
            }
            else
                return true;                                            // If the passwords match, return true.
        }
    } 
}
