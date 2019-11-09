using System;
using System.Security.Cryptography;
using System.Text;

namespace E_Hospital.BLL.Utils
{
    public static class EncryptionUtil
    {
        public static string GenerateSalt()
        {
            var rng  = new RNGCryptoServiceProvider();
            var buff = new byte[128 / 8];

            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public static string HashPassword(string password, string salt)
        {
            var bytes            = Encoding.UTF8.GetBytes(password + salt);
            var sha256ManagedStr = new SHA256Managed();
            var hash             = sha256ManagedStr.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}