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

        public bool result;                                     // Authentication result 
        public string message;                                  // Authentication message

        public void AuthenticationSequence(User _user)
        {
            loginUser = _user;                  // User login details from the View 'Login'  

                                                // Call methods to authenticate the user
            if (!ConnectToMysql()
                || !GetUserData()
                || !ComparePasswords())
                return;                         //  If any the above methods fail, return to the calling class Logn.xaml
            else
            {
                result = true;                  // Otherwise set result to true 
                return;                         //  and return to the caller. 
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
                result=false;               //  set result to false
                message = ex.Message;       //  and set the error message
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
                result=false;               //  set result to false
                message =  ex.Message;      //  and set the error message
                return false;
            }

            // Create the SELECT sqlCmd, that will be sent to the database,
            //  returning the user's email address, password and createDate
            string sqlCmd =
                "SELECT email, password, createDate " +
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
                            result = false;                                 //  set result to false
                            message = "Email does not exist";               //  and set the error message
                            return false;                                       //  and return
                        }

                        if (reader.Read())                                      // If the user id found, add the returned data to databaseUser properties
                        {
                            databaseUser.Email      = GetColumnValueAsString(reader, "email");          // To prevent 'null errors' when data is returned from the database 
                            databaseUser.Password   = GetColumnValueAsString(reader, "password");       //  the method GetColumnValueAsString() converts null values 
                            databaseUser.CreateDate = GetColumnValueAsString(reader, "createDate");     //  to an empty string 
                        }
                    }
                }
                catch(Exception ex)                 // If getting a valid row from the database does not work
                {
                    result = false;             //  set result to false
                    message = ex.Message;       //  and set the error message
                    return false;
                }
            }
            return true;
        }


        // GetColumnValueAsString()
        // To prevent 'null errors' when values are returned from the database, convert them to empty strings.
        string GetColumnValueAsString(MySqlDataReader reader, string colName)
        {
            if (reader[colName] == DBNull.Value)
                return string.Empty;
            else
                return reader[colName].ToString();
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
                result = false;                                     //  set result to false
                message = "Incorrect password";                     //  and set the error message
                return false;                                             
            }
            else
                return true;                                            // If the passwords match, return true.
        }
    } 
}
