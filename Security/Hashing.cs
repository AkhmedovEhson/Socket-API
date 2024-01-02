using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketClient.Security
{
    public class Hashing
    {
        public string Encryption(string message)
        {
            Aes aes = Aes.Create();
            string key = "0123456789abcdef0123456789abcdef"; // 256-bit key
            string iv = "fedcba9876543210";
            aes.Key = Encoding.ASCII.GetBytes(key);
            aes.IV = Encoding.ASCII.GetBytes(iv);
            var encryptor = aes.CreateEncryptor();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(message);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        public string Decryption(string hash)
        {
            using (Aes aesAlg = Aes.Create())
            {
                string response = string.Empty;
                string key = "0123456789abcdef0123456789abcdef"; // 256-bit key
                string iv = "fedcba9876543210";
                aesAlg.Key = Encoding.ASCII.GetBytes(key);
                aesAlg.IV = Encoding.ASCII.GetBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(hash)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            response = srDecrypt.ReadToEnd();
                        }
                    }
                }
                return response;
            }
        }
    }
}
