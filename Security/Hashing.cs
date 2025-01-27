
using System.Security.Cryptography;
using System.Text;
using Security;
using Security.Configurations;
using Security.Dtos;
using Serilog;
namespace SocketClient.Security;
/// <summary>
/// Component `Hashing`, provides bunch of APIs for hashing
/// </summary>
public class Hashing
{
    private Configuration Configuration = new Configuration(Constants.BasePath, "appsettings.json");
    /// <summary>
    /// Symmetric encryption, encrypts message with specific ( secretkey ) else throws `<seealso cref="Exception"/>`
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="Exception"/>
    /// <returns></returns>
    public string Encryption(string message)
    {
        using Aes aes = Aes.Create();
        var obj = Configuration.GetObject<SecurityModel>();

        string key = obj.Security.Aes.Key;
        string iv = obj.Security.Aes.Iv; 

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
        var obj = Configuration.GetObject<SecurityModel>();

        string response = string.Empty;
        string key = obj.Security.Aes.Key;
        string iv = obj.Security.Aes.Iv; 

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
