using System.Collections.Generic;
using System.Text;
using TUFCv3.Additional.Archive;
using TUFCv3.Additional.MySql;
using MySqlConnector;
using System;
using TUFCv3.Models.Users;

namespace TUFCv3.Additional.MySql
{
    public class GetLoginDetails : IGetMySqlData
    {
        MySql.IConnection mySqlConn = new MySql.Connection();       // Connects to the database
        MySqlDataReader reader;                                     // Reads from the database

        User databaseUser = new User();                             // User data, retrieved from the database.

        public string errorMessage { get; set; }                    // Error message.


        /*  RunQuery() 
            Calls the other methods in this class, to get the loginUser's data. */
        public User RunQuery(IUser loginUser)
        {
            if (!OpenConnection()
                || !GetData(loginUser)
                || !ConvertToProperties())
            {
                databaseUser = null;
            }
            return databaseUser;
        }



        // OpenConnection()
        /*  Open the connection to the database */
        public bool OpenConnection()
        {
            if (mySqlConn.OpenConnection() == false)         // If a connection to the database can not be opened: 
            {
                errorMessage = mySqlConn.errorMessage;      //  - set the error message,
                return false;                               //  - and return false.
            }
            return true;                                    // If the connection is okay, return true.
        }


        // GetData()
        /*  Send the SQL command, to get the users details
            the returned data is stored as the public variable 'reader' */
        public bool GetData(IUser loginUser)
        {
            string sqlCmd =                                         // Create the MySQL query, to get loginUser info from the database.
                "SELECT email, password, createDate " +
                "FROM User " +
                "WHERE email = @email ";

            using (MySqlCommand command = new MySqlCommand(sqlCmd, mySqlConn.connection))       // Create a MySql command,
            {
                command.Parameters.Add(new MySqlParameter("@email", loginUser.Email));          //  including the @email parameter.

                try
                {
                    reader = command.ExecuteReader();               // Send the sqlCmd to the MySQL database
                }
                catch (Exception ex)                                // If getting a valid row from the database does not work
                {
                    mySqlConn.errorMessage = ex.Message;            //  and set the error errorMessage
                    mySqlConn.connection.Close();
                    return false;
                }

                return true;
            }
        }


        // ConvertToProperties()
        /*  Add the loginUser details, returned from the database, 
            to properties in the object 'databaseUser' */
        public bool ConvertToProperties()
        {
            try
            {
                if (reader.Read())                                              // If the loginUser is found, add the returned data to databaseUser properties
                {
                    databaseUser.Email      = reader.GetValue(0).ToString();    // There is an alternative method GetString(),  
                    databaseUser.Password   = reader.GetValue(1).ToString();    //  but it causes errors when null values 
                    databaseUser.CreateDate = reader.GetValue(2).ToString();    //  are returned from the database.
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

            mySqlConn.connection.Close();
            return true;
        }
    }
}
