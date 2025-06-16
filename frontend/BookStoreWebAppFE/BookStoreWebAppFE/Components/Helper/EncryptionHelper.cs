using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace BookStoreWebAppFE.Components.Helper
{
    public static class EncryptionHelper
    {
        private static readonly string EncryptionKey = "vMZ8Y3+pqO6JWxh3/U+M4PfJbV9zz9JX+0ThQ9AV1Qo=";

        private static byte[] GetKeyBytes(string key)
        {

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, 32);
            return keyBytes;
        }

        public static string Encrypt(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetKeyBytes(EncryptionKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new((Stream)cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = GetKeyBytes(EncryptionKey);
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new((Stream)cryptoStream);
            return streamReader.ReadToEnd();
        }

        public static string SerializeAndEncrypt<T>(T data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            return Encrypt(jsonString);
        }

        public static T DecryptAndDeserialize<T>(string cipherText)
        {
            string json = Decrypt(cipherText);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
