using System;
using System.Security.Cryptography;
using System.Text;

namespace DS.Kids.Model.Services.Security
{
    /// <summary>
    /// Criptografia com o Algorítimo SHA512
    /// </summary>
    public class SHA512
    {
        /// <summary>
        /// Criptografar
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ComputeHash(string value)
        {
            Throw.IfIsNullOrEmpty(value);

            var sha512 = new SHA512Managed();
            byte[] hash = sha512.ComputeHash(Encoding.ASCII.GetBytes(value));

            var sb = new StringBuilder();
            foreach (byte b in hash)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }
    }
}
