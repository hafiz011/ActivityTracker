using System.Security.Cryptography;
using System.Text;

namespace ActivityTracker.Infrastructure.Services
{
    public class ApiKeyGenerator
    {
        public static (string RawKey, string HashedKey) GenerateApiKey()
        {
            var rawKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)); // Secure 256-bit key
            var hashedKey = HashApiKey(rawKey);
            return (rawKey, hashedKey);
        }

        public static string HashApiKey(string rawKey)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(rawKey);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
