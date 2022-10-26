using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUFCv3;
using TUFCv3.Additional.AuthenticateUser;
using TUFCv3.Models.Users;
using Xunit;
using Xunit.Sdk;

namespace UnitTests
{
    public class AuthenticateUser_Tests
    {
        // Instantiate the class being tested  
        public IAuthenticateUser authenticateUser = Factory.CreateAuthenticateUser();
        

        [Fact]
        public void GetDatabaseUser_ShouldReturnTrue()
        {
            // Arrange            
            bool expected = true;      

            // Act            
            IUser testUser = Factory.CreateUser();                          // Create the methods User argument
            testUser.Email = "user@mail.com";                               //  including the email property 'user@mail.com'

            //  - call the method being tested
            bool actual = authenticateUser.GetDatabaseUser(testUser);       // Call the method being tested

            // Assert
            Assert.Equal(expected, actual);                                 // In a passing test 'expected' and 'actual' both equal 'true'.
        }


        [Fact]
        public void ComparePasswords_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;                                                   // Expected return value from ComparePasswords()

            IUser loginUser = Factory.CreateUser();                                 // Create the IUser argument required for ComparePasswords()
            loginUser.Password = "user123";                                         //  and set the unencrypted password.

            authenticateUser.databaseUser.Password = "AvkkK+XT3MIvTlW2ivlooA==";    // Set the mock databaseUser.Password (encrypted)


            // Act 
            bool actual = authenticateUser.ComparePasswords(loginUser);             // Call the method being tested


            //Assert
            Assert.Equal(expected, actual);                                         // In a passing test 'expected' and 'actual' both equal 'true'.
        }
    }
} 
