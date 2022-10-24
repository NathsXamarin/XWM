namespace TUFCv3.Additional.Encryption
{
    public interface IEncryption
    {
        string DecryptText(string cipherText);      // Decrypt an encryted string
        string EncryptText(string plainText);       // Encrypt plain text
    }
}
