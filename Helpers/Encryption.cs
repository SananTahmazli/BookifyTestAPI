using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Encryption
    {
        public static string GenerateSalt()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[255];
                provider.GetNonZeroBytes(saltBytes);
                string salt = Convert.ToBase64String(saltBytes);
                return salt;
            }
        }

        public static string GenerateHash(string password, string salt)
        {
            using (var algorithm = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
            {
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Encoding.UTF8.GetString(hash);
            }
        }
    }
}