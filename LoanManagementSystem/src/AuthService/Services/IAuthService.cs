using AuthService.DTOs;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterRequest registerRequest);

        Task<bool> ValidateUserAsync(string email, string password);

        Task<string?> LoginAsync(LoginRequest loginRequest);
    }
}
