using LoanService.Data;
using LoanService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _context;

        public LoanRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Loan>> GetLoansByCustomerAsync(int customerId)
        {
            var loans = await _context.Loans.Where(l => l.CustomerId == customerId).ToListAsync();

            return loans;
        }
    }
}
