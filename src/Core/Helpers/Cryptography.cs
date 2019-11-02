using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Core.Helpers
{
    public class Cryptography
    {
         
        public static bool Validate(string value, string salt, string hash) => CreateHash(value, salt) == hash;

        public static string CreateHash(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(  
                password: value,  
                salt: Encoding.UTF8.GetBytes(salt),  
                prf: KeyDerivationPrf.HMACSHA512,  
                iterationCount: 10000,  
                numBytesRequested: 256 / 8);  
  
            return Convert.ToBase64String(valueBytes); 
        }

        public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];  
            using (var generator = RandomNumberGenerator.Create())  
            {  
                generator.GetBytes(randomBytes);  
                return Convert.ToBase64String(randomBytes);  
            }  
        }
    }
}