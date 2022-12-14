using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TUFCv3.Additional.Encryption
{
    public class EncryptionCbc : IEncryptionCbcCombo
    {
        public Aes encryptor { get; set; }          // The encryption object, configured for Cipher Block Chain.    
        public string Password { get; set; }        // CBC password
        public string Iv { get; set; }              // CBC IV



        /*  Constructor 
            Get the Password and IV
            Call the method CreateEncryptor() to creaete a CBC encryptor  */
        public EncryptionCbc(string _password = "password!", string _iv = "TopSecretVector!")
        {
            string Password = _password;                    // Make the password argument public                 
            string Iv = _iv;                                // Make the IV argument public (must be 16 characters)

            CreateEncryptor(Password, Iv);                  // Create the object 'encryptor'                
            // TestEncryption("Message to encrypt/decrypt");   // Test the encryption method ('comment out', but do not delete) 

            TestEncryption("Decrypted message");   // Test the encryption method ('comment out', but do not delete) 
        }



        /*  CreateEncryptor()
            Create and configure the object 'encryptor'
            that encodes/decodes messages  */
        public void CreateEncryptor(string Password, string Iv)
        {
            encryptor = Aes.Create();                                               // Create the AES encryption object.
            encryptor.Mode = CipherMode.CBC;                                        // Set encryption method to Cyclic Block Chain.

            SHA256 sha256 = SHA256.Create();                                 // Set the Key.
            byte[] key = sha256.ComputeHash(Encoding.ASCII.GetBytes(Password));
            encryptor.Key = key;

            byte[] ivBytes = Encoding.ASCII.GetBytes(Iv);                           // Set the IV.
            encryptor.IV = ivBytes;
        }



        /*  EncryptText()
            Encrypt text  */
        public string EncryptText(string plainText)
        {
            MemoryStream memoryStream = new MemoryStream();                                                     // Memory stream
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();                                        // Set the transform to encrypt
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);   // EncryptionCbc stream

            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);     // Convert the original string to the byte array 'plainbytes' 
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);       // Encrypt the array using CBC
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);     // Convert the encrypted byte array to a string

            return cipherText;
        }


        /* DecryptText()
            Decrypt encrypted text  */
        public string DecryptText(string cipherText)
        {
            string plainText = string.Empty;        // Will contain the decrypted text

            MemoryStream memoryStream = new MemoryStream();                                                     // Memory stream
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();                                        // Set the transform to decrypt
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);   // EncryptionCbc stream

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);      // Convert the original string to the byte array 'cipherBytes' 
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);         // Decrypt the cipherBytes using CBC
                cryptoStream.FlushFinalBlock();

                byte[] plainBytes = memoryStream.ToArray();                                 // Write the decrypted bytes to an array
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);     // Convert the decrypted byte array to a string
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }

            return plainText;
        }


        /*  TestEncryption()  
            Only used during manual testing */
        public void TestEncryption(string message)
        {
            string encryptedMessage = EncryptText(message);                 // Encrypt a errorMessage
            string decryptedMessage = DecryptText(encryptedMessage);        // Decrypt the errorMessage
        }
    }
}
