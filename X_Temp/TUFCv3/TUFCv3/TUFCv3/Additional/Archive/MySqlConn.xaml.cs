using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TUFCv3.Additional.Archive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MySqlConn : ContentPage
    {
        public MySqlConnection connection = new MySqlConnection();

        public MySqlConn()
        {
            InitializeComponent();
            ConnectToMysql();
        }

        public void ConnectToMysql()
        {
            // Create a MySqlConnection, using the server xwm-mysql's conn details  (additional options:  Port=3306; SslMode=none)
            connection = new MySqlConnection(
                "Server=xwm-mysql;"+
                "Database=tufc;" +
                "User ID=admin;" +
                "Password=adm1n;"
                );

            try
            {
                connection.Open();
                DisplayAlert("Connection", "Connected to the database xwm-mysql", "Okay");
                connection.Close();
            }
            catch (Exception ex)
            {
                DisplayAlert("Connection", ex.Message, "Okay");
            }
        }

        private async void OnInsertClick(object sender, EventArgs e)
        {
            // Connect to the server
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Connection", ex.Message, "Okay");
                return;
            }


            // Create an execute an INSERT command 
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO User (email, createDate) VALUES (@e, @d)";       // Create a query to save data to MySQL
                cmd.Parameters.AddWithValue("e", email.Text);                                   // String from the xaml entry 'email'      
                cmd.Parameters.AddWithValue("d", DateTime.Now);                                 // Current time, obtained using the function DateTime()        

                // Save the data
                try
                {
                    cmd.ExecuteNonQuery();
                    await DisplayAlert("Insert data", email.Text + " inserted \ninto the table 'User'", "Okay");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Insert data", ex.Message, "Okay");
                }
            }

            connection.Close();
        }

        private async void OnUpdateClick(object sender, EventArgs e)
        {
            // Connect to the server
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Connection", ex.Message, "Okay");
                return;
            }

            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = connection;

                // Create the command UPDATE 
                cmd.CommandText = "UPDATE User " +
                    "SET email = @newEmail " +
                    "WHERE email = @oldEmail ";
                cmd.Parameters.AddWithValue("@newEmail", newEmail.Text);
                cmd.Parameters.AddWithValue("@oldEmail", email.Text);

                // Execute the command
                try
                {
                    cmd.ExecuteReader();
                    await DisplayAlert("Connection", email.Text + " updated to " + newEmail.Text + "\non the database 'xwm-mysql' ", "Okay");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Connection", ex.Message, "Okay");
                }
            }

            connection.Close();
        }

        private async void OnDeleteClick(object sender, EventArgs e)
        {
            // Connect to the server
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Connection", ex.Message, "Okay");
                return;
            }

            using (var cmd = new MySqlCommand())
            {
                // Create the command to DELETE
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM User " +
                    "WHERE email = @e";
                cmd.Parameters.AddWithValue("@e", email.Text);

                // Execute the query
                try
                {
                    cmd.ExecuteReader();
                    await DisplayAlert("Connection", "Deleted " + email.Text + "\nfrom 'Ubuntu-MySQL' ", "Okay");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Connection", ex.Message, "Okay");
                }

                connection.Close();
            }
        }

        private async void OnSelectClick(object sender, EventArgs e)
        {
            // Connect to the server
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Connection", ex.Message, "Okay");
                return;
            }

            var query = new MySqlCommand(                           // Create the query
                "SELECT *" +
                "FROM User",
                connection);

            var reader = await query.ExecuteReaderAsync();

            string displayedString = "";

            while (await reader.ReadAsync())
            {
                string email = reader.GetString(1);                 // User's email, which is already a string
                string dateTime = reader.GetValue(3).ToString();    // Creation date (DateTime) 

                // Create the string to be displayed
                displayedString +=
                    "\n   Email: " + email +
                    "\n   Created: " + dateTime + "\n";
            }

            lblUsers.Text = displayedString;

            connection.Close();                                     // Close the conn
        }
    }
}