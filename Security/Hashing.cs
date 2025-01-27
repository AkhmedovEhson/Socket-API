
using System.Security.Cryptography;
using System.Text;
using Serilog;
namespace SocketClient.Security;
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
        string key = "0123456789abcdef0123456789abcdef"; // 256-bit key (32 characters)
        string iv = "fedcba9876543210"; // 128-bit IV (16 characters)
        aes.Key = Encoding.ASCII.GetBytes(key);
        aes.IV = Encoding.ASCII.GetBytes(iv);

        using MemoryStream msEncrypt = new MemoryStream();
        using CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        {
            // Write the message to the CryptoStream
            swEncrypt.Write(message);
        }

        // At this point, the streams are flushed and disposed
        return Convert.ToBase64String(msEncrypt.ToArray());
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
        string key = "0123456789abcdef0123456789abcdef"; // 256-bit key (32 characters)
        string iv = "fedcba9876543210"; // 128-bit IV (16 characters)
        aesAlg.Key = Encoding.ASCII.GetBytes(key);
        aesAlg.IV = Encoding.ASCII.GetBytes(iv);

        try
        {
            using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(hash));

            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read);

            using StreamReader srDecrypt = new StreamReader(csDecrypt);

            response = srDecrypt.ReadToEnd();
        }
        catch (Exception ex)
        {

            throw;
        }

        return response;
    }
}
