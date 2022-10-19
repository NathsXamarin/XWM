using System.Security.Cryptography;

namespace TUFCv3.Additional.Encryption
{
    public interface IEncryptionCbc
    {
        Aes encryptor { get; set; }                             // Encryptor used in Cyclic Block Cypher (CBC) encryption.
        string Iv { get; set; }                                 // Initialization Vector
        string Password { get; set; }                           // Password

        void CreateEncryptor(string Password, string Iv);       // Create the property 'encryptor', using the password and iv.
    }
}