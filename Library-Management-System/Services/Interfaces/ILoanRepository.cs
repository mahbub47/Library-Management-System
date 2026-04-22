using Library_Management_System.Entities;
using Library_Management_System.Services.Dtos;

namespace Library_Management_System.Services.Interfaces;

public interface ILoanRepository
{
    Task<Loan> AddLoanAsync(Loan loan);
    Task<Loan> UpdateLoanAsync(Loan loan);
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<Loan> GetLoanByIdAsync(int loanId);
    Task DeleteLoanAsync(Loan loan);
    Task<IEnumerable<Loan>> SearchLoanAsync(string? bookName, string? memberName, int? bookId, int? memberId, string? authorName, string? isbn);
}
