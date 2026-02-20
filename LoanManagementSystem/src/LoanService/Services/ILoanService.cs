using LoanService.Models;

namespace LoanService.Services
{
    public interface ILoanService
    {
        Task CreateLoanAsync(int customerId, decimal Amount);
        Task<List<Loan>> GetLoansAsync(int customerId);
    }
}
