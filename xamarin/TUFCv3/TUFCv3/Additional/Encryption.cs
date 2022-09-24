using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TUFCv3.Additional
{
    public class Encryption
    {      
        Aes encryptor;      // The encryption object, configured for CBC (includes the key and IV) 

        public Encryption()
        {
            string password = "password!";                  
            string iv = "TopSecretVector!";     // On a live system 'iv' should be randomly generated and saved to a secure location.
         
            CreateEncryptor(password, iv);      // Create the object 'encryptor'                
            TestEncryption();                   // Test the encryption method ('comment out', but do not delete) 
        }


        // CreateEncryptor()
        // Create the object 'encryptor', that will be used to encode/decode messages   
        void CreateEncryptor(string password, string iv)
        {
            SHA256 sha256 = SHA256Managed.Create();                                 // Create a Secure Hashing Algorithm
            byte[] key = sha256.ComputeHash(Encoding.ASCII.GetBytes(password));     // Create the hash key, using 'password'
            byte[] ivBytes = Encoding.ASCII.GetBytes(iv);                           // Convert the 'iv' to a byte array
                                                                            
            encryptor = Aes.Create();               // Create the Advanced Encryption Standard (AES) object 'encryptor'
            encryptor.Mode = CipherMode.CBC;        // Set encryption method to Cyclic Block Chain (CBC)

            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);      // Copy the hashed password 'key' to the byte array 'aesKey'  
            encryptor.Key = aesKey;                 // Set 'aesKey' as the the encryption key 
            encryptor.IV = ivBytes;                 //  and ivBytes as the initialization vector 
        }


        // EncryptString()
        // Encrypt a message
        public string EncryptString(string plainText)
        {
            MemoryStream memoryStream = new MemoryStream();                                                     // Memory stream
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();                                        // Set the transform to encrypt
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);   // Encryption stream

            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);     // Convert the original string to the byte array 'plainbytes' 
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);       // Encrypt the array using CBC
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();                

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);     // Convert the encrypted byte array to a string

            return cipherText;
        }


        // DecryptString()
        string DecryptString(string cipherText)
        {
            MemoryStream memoryStream = new MemoryStream();                                                     // Memory stream
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();                                        // Set the transform to decrypt
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);   // Encryption stream

            string plainText = String.Empty;        // Will contain the decrypted plain text

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


        // TestEncryption() - only used during testing 
        void TestEncryption()
        {
            string encryptedMessage = this.EncryptString("Message to encode/decode");       // Encrypt a message
            string decryptedMessage = this.DecryptString(encryptedMessage);                 // Decrypt the message
        }
    }
}
