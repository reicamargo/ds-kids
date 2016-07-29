using System;
using System.IO;
using System.Security.Cryptography;

namespace DS.Kids.Model.Services.Security
{
    public class Rijndael
    {
        private static byte[] KEY = { 206, 0, 252, 127, 232, 201, 148, 112, 140, 74, 11, 61, 235, 158, 251, 205, 39, 21, 187, 235, 221, 10, 220, 117, 24, 184, 134, 184, 130, 95, 47, 177 };
        private static byte[] IV = { 200, 171, 138, 208, 93, 25, 139, 83, 208, 201, 10, 116, 225, 160, 196, 210 };

        public static string Encrypt(string value)
        {
            using (var rijndael = System.Security.Cryptography.Rijndael.Create())
            {
                rijndael.Key = KEY;
                rijndael.IV = IV;

                var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                var sw = new StreamWriter(cs);
                sw.Write(value);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Decrypt(string value)
        {
            var cipherText = System.Convert.FromBase64String(value);
            using (var rijndael = System.Security.Cryptography.Rijndael.Create())
            {
                rijndael.Key = KEY;
                rijndael.IV = IV;

                var decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                var ms = new MemoryStream(cipherText);
                var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
        }
    }
}
