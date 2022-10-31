using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using TUFCv3.Additional.MySql;
using Xunit;
using TUFCv3.Models.Users;
using MySqlConnector;
using TUFCv3.Additional.Archive;

namespace UnitTests
{
    public class GetLoginDetails_Tests
    {
        public GetLoginDetails getLoginDetails = new GetLoginDetails();

        [Fact]
        void OpenConnection_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;                               // A passing test should return the value true
            

            // Act
            bool actual = getLoginDetails.OpenConnection();     // Call the method being tested


            // Assert
            Assert.Equal(expected, actual);                     // In a passing test 'expected' and 'actual' both equal 'true'.



            // Close the connection
            getLoginDetails.CloseConnection();                  // Close the connection to the database 'xwm-mysql'
        }



        [Fact]
        void GetData_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;                               // A passing test should return the value true

            bool connected = getLoginDetails.OpenConnection();  // Open a connection to the database

            IUser user = new User();                            // Create an IUser, as the test methods argument
            user.Email = "user@mail.com";                       //  with the email address 'user@mail.com'.


            // Act
            bool actual = getLoginDetails.GetData(user);        // Call the method being tested
            

            // Assert
            Assert.Equal(expected, actual);                     // In a passing test 'expected' and 'actual' both equal 'true'.


            // Close the connection
            getLoginDetails.CloseConnection();                  // Close the connection to the database 'xwm-mysql'
        }


        [Fact]
        void ConvertToProperties_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;                                   // A passing test should return the value true

            IUser user = new User();                                // Create an IUser, as the test methods argument
            user.Email = "user@mail.com";                           //  with the email address 'user@mail.com'.

            bool connected = getLoginDetails.OpenConnection();      // Open a connection to the database
            bool getData = getLoginDetails.GetData(user);           // Call get data to get user properties from the database

            // Act
            bool actual = getLoginDetails.ConvertToProperties();    // Call the method being tested


            // Assert
            Assert.Equal(expected, actual);                         // In a passing test 'expected' and 'actual' both equal 'true'.


            // Close the connection
            getLoginDetails.CloseConnection();                      // Close the connection to the database 'xwm-mysql'
        }
    }
}
