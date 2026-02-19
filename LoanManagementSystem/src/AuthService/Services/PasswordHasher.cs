using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services
{
    public class PasswordHasher
    {
        // used for POC and it is a custom password hash logic
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
