using LoanService.Models;
using LoanService.Repositories;

namespace LoanService.Services
{
    public class LoanManagementService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanManagementService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task CreateLoanAsync(int customerId, decimal Amount)
        {
            var loan = new Loan
            {
                CustomerId = customerId,
                Amount = Amount,
                Status = "Pending"
            };

            await _loanRepository.AddLoanAsync(loan);
        }

        public async Task<List<Loan>> GetLoansAsync(int customerId)
        {
            return await _loanRepository.GetLoansByCustomerAsync(customerId);
        }
    }
}
