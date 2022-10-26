using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using TUFCv3.Additional.Archive;

namespace TUFCv3.Additional.MySql
{
    public class Connection : IConnection
    {
        public MySqlConnection connection { get; set; }     // Connection to the MySQL database 
        public string errorMessage { get; set; }            // Error message

        public Connection()
        {
            ConnectionString();                 // Create the connection string
        }

        /*  ConnecionString()
            Create the connection string for the MysQL server 'xwm-mysql'  */
        public void ConnectionString()
        {
            connection = new MySqlConnection
                (
                    "Server=xwm-mysql; " +
                    "Database=tufc; " +
                    "User ID=admin; " +
                    "Password=adm1n; "
                );
        }



        /*  TestConnection()
            To make sure there is communication with the database
            open and close the connection  */
        public bool TestConnection()
        {
            try
            {                                       // Test the connection by:
                connection.Open();                  //  - opening the connection,              
                connection.Close();                 //  - then closing it, 
                return true;                        //  - return True.
            }
            catch (Exception ex)                    // If the connection fails:
            {
                errorMessage = ex.Message;          //  - set the error errorMessage,
                return false;                       //  - return False.
            }
        }



        /*  OpenConnection()
            Open the connection, ready for queries */
        public bool OpenConnection()
        {
            try
            {                                       // Try to connect:
                connection.Open();                  //  - open the connection,
                return true;                        //  - return True.
            }
            catch (Exception ex)                    // If a connection can not be established:
            {
                errorMessage =  ex.Message;         //  - set the error errorMessage,
                return false;                       //  - and return false
            }
        }
    }
}

