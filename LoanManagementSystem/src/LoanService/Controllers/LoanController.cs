using LoanService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("create-loan")]
        public async Task<IActionResult> CreateLoan(decimal amount)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _loanService.CreateLoanAsync(userId, amount);

            return Ok("Loan created Successfully");
        }

        [HttpGet("get-loans")]
        public async Task<IActionResult> GetLoans()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var loans = await _loanService.GetLoansAsync(userId);
            return Ok(loans);
        }
       
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Message = "JWT validated Successfully",
                UserId = userId,
                Email = email,
                Role = role
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("Only admins can see this");
        }
    }
}
