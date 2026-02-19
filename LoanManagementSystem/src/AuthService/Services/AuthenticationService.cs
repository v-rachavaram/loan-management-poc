using AuthService.DTOs;
using AuthService.Models;
using AuthService.Repositories;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService _passwordService;

        public AuthenticationService(IUserRepository userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<string> RegisterUserAsync(RegisterRequest registerRequest)
        {
           var existingUser = await _userRepository.GetByEmailAsync(registerRequest.Email);

            if (existingUser != null)
            {
                return "Email already exists";
            }

            var user = new User
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                Role = registerRequest.Role
            };

            user.PasswordHash = _passwordService.HashPassword(user, registerRequest.Password);

            await _userRepository.AddUserAsync(user);

            return "User registered successfully";
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null) return false;

            return _passwordService.VerifyPassword(user, password);
        }
    }
}
