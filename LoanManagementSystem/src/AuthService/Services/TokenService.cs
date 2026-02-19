using AuthService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var settings = _configuration.GetSection("JwtSettings");

            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings["Key"]!));

            //Signing Algorithm
            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var jwt = new JwtSecurityToken(
                issuer: settings["Issuer"],
                audience: settings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(settings["ExpiryMinutes"])),
                signingCredentials: signingCreds
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
