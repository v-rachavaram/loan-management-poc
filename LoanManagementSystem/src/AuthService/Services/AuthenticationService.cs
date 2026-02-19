using AuthService.DTOs;
using AuthService.Models;
using AuthService.Repositories;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUserRepository userRepository
            ,PasswordService passwordService
            ,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<string?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

            if (user == null) return null;

            var isValid = _passwordService.VerifyPassword(user, loginRequest.Password);

            if (!isValid)
            {
                return null;
            }

            var token = _tokenService.GenerateToken(user);

            return token;

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
