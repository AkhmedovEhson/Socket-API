
using System.Security.Cryptography;
using System.Text;
using Security.Utils;

namespace SocketClient.Security;
using static CustomLogger;
/// <summary>
/// Component `Hashing`, provides bunch of APIs for hashing
/// </summary>
public class Hashing
{
    /// <summary>
    /// Symmetric encryption, encrypts message with specific ( secretkey ) else throws `<seealso cref="Exception"/>`
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="Exception"/>
    /// <returns></returns>
    public string Encryption(string message)
    {
        using Aes aes = Aes.Create();

        const string key = "0123456789abcdef0123456789abcdeg"; // 256-bit key
        const string iv = "fedcba9876543210";

        aes.Key = Encoding.ASCII.GetBytes(key);
        aes.IV = Encoding.ASCII.GetBytes(iv);

        try
        {
            var encryptor = aes.CreateEncryptor();

            using MemoryStream msEncrypt = new();

            using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);

            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(message);
            }           
            return Convert.ToBase64String(msEncrypt.ToArray());
        }
        catch (Exception ex)
        {
            Logger.Information(nameof(Encryption),ex.Message);
            throw;
        }
    }
    /// <summary>
    /// Decrypts AES encrypion using specified ( secret key ), else throws `<seealso cref="Exception"/>`
    /// </summary>
    /// <param name="hash"></param>
    /// <exception cref="Exception"/>
    /// <returns></returns>
    public string Decryption(string hash)
    {
        using Aes aesAlg = Aes.Create();
        string response = string.Empty;

        const string key = "0123456789abcdef0123456789abcdeg"; // 256-bit key
        const string iv = "fedcba9876543210";

        aesAlg.Key = Encoding.ASCII.GetBytes(key);
        aesAlg.IV = Encoding.ASCII.GetBytes(iv);

        try
        {
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(hash));

            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                response = srDecrypt.ReadToEnd();
            }
            
        }
        catch (Exception ex)
        {
            Logger.Information(nameof(Decryption),ex.Message);
            throw;
        }

        return response;
    }
}
