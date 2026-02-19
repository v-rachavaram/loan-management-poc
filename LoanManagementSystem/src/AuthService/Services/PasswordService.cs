using AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Services
{
    public class PasswordService
    {
        private readonly IPasswordHasher<User> _hasher;

        public PasswordService(IPasswordHasher<User> hasher)
        {
            _hasher = hasher;
        }

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string password)
        {
            var result = _hasher.VerifyHashedPassword(user,user.PasswordHash,password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
