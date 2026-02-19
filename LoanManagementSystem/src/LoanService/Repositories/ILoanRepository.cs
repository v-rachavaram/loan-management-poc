using LoanService.Models;

namespace LoanService.Repositories
{
    public interface ILoanRepository
    {
        Task AddLoanAsync(Loan loan);
        Task<List<Loan>> GetLoansByCustomerAsync(int customerId);
    }
}
