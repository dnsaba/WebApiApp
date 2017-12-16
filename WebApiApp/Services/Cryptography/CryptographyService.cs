using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebApiApp.Services.Cryptography
{
    public class CryptographyService
    {
        public string GenerateRandomString(int length)
        {
            // This will give us approximately the desired length string.
            byte[] bytes = new byte[(int)Math.Floor(length * .75)];

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes);
        }

        public string Hash(string original, string salt, int iterations = 1)
        {
            const int hashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash

            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] bytes;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(original, saltBytes))
            {
                pbkdf2.IterationCount = iterations;
                bytes = pbkdf2.GetBytes(hashByteSize);
            }

            return Convert.ToBase64String(bytes);

        }
    }
}