using Library_Management_System.Entities;
using Library_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryManagementDBContext _context;
    public LoanRepository(LibraryManagementDBContext context) => _context = context;
    public async Task<Loan> AddLoanAsync(Loan loan)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        await _context.AddAsync(loan);
        await _context.Books.Where(b => b.Id == loan.BookId).ExecuteUpdateAsync(b => b.SetProperty(b => b.AvailableCopies, b => b.AvailableCopies - 1));

        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return loan;
    }

    public async Task DeleteLoanAsync(Loan loan)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        _context.Remove(loan);
        await _context.Books.Where(b => b.Id == loan.BookId).ExecuteUpdateAsync(b => b.SetProperty(b => b.AvailableCopies, b => b.AvailableCopies + 1));
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return await _context.Loans.Include(l => l.Book).Include(l => l.Member).ToListAsync();
    }

    public async Task<Loan> GetLoanByIdAsync(int loanId)
    {
        var loan = await _context.Loans.Include(l => l.Member).Include(l => l.Book).Where(l => l.Id == loanId).FirstOrDefaultAsync();
        if (loan == null) return null!;
        return loan;
    }

    public async Task<Loan> UpdateLoanAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
        return loan;
    }
}
