using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TUFCv3;
using TUFCv3.Additional.Encryption;
using Xunit;

namespace UnitTests
{
    public class EncryptionCbc_Tests
    {
        IEncryptionCbcCombo encryptionCbc = Factory.CreateEncryptionCbcCombo();     // Instantiate the class being tested

        [Fact]
        public void EncryptText_CiperShouldMatch()
        {
            // Arrange
            string expected = "ezYk0ahxG120zw2P8Ry11TL1spCRF/o6uztKYr+fobE=";       // Encrypted message 
            string plainText = "Message to encrypt";                                // Unencrypted message


            // Act
            string actual = encryptionCbc.EncryptText(plainText);                   // Call EncryptText() to encrypt the message

            // Assert
            Assert.Equal(expected, actual);                                         // The strings 'expected' and 'actual'
                                                                                    //  should both be encrypted to the same cipher.
        }


        [Fact]
        public void DecryptText_DecryptedTextShouldMatch()
        {
            // Arrange
            string expected = "Decrypted message";                                  // Decrypted message 
            string cipherText = "qojihVqnsiYkKqwcwlNge7Qr4/Cb2IZ+515bqpZdCVw=";     // String to decrypt


            // Act
            string actual = encryptionCbc.DecryptText(cipherText);                   // Call DecryptText() to decrypt the message

            // Assert
            Assert.Equal(expected, actual);                                         // The strings 'expected' and 'actual'
                                                                                    //  should both be the same plain text.
        }
    }
}
