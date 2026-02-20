using System.Security.Claims;

namespace LoanService.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                throw new UnauthorizedAccessException("User ID claim is missing");
            }

            if(!int.TryParse(claim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid User Id claim");
            }

            return userId;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value ??
                throw new UnauthorizedAccessException("Email claim is missing");
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value ??
                throw new UnauthorizedAccessException("Role claim is missing");
        }
    }
}
