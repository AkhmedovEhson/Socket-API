
using System.Security.Cryptography;
using System.Text;
using Serilog;
namespace SocketClient.Security;
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
        Aes aes = Aes.Create();
        string key = "0123456789abcdef0123456789abcdeg"; // 256-bit key
        string iv = "fedcba9876543210";
        aes.Key = Encoding.ASCII.GetBytes(key);
        aes.IV = Encoding.ASCII.GetBytes(iv);

        try
        {
            var encryptor = aes.CreateEncryptor();

            using MemoryStream msEncrypt = new MemoryStream();

            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            using StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(message);
            return Convert.ToBase64String(msEncrypt.ToArray());
        }
        catch (Exception ex)
        {
            Log.Information(ex.Message);
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
        using (Aes aesAlg = Aes.Create())
        {
            string response = string.Empty;
            string key = "0123456789abcdef0123456789abcdeg"; // 256-bit key
            string iv = "fedcba9876543210";
            aesAlg.Key = Encoding.ASCII.GetBytes(key);
            aesAlg.IV = Encoding.ASCII.GetBytes(iv);

            try
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(hash));

                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                response = srDecrypt.ReadToEnd();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            return response;
        }
    }
}
