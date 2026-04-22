using Library_Management_System.Services.Dtos;

namespace Library_Management_System.Services.Interfaces;

public interface ILoanManagementService
{
    Task<LoanResponseDto> CreateLoanAsync(CreateLoanDto dto);
    Task<bool> CancelLoanAsync(int loanId);
    Task<LoanResponseDto> UpdateLoanAsync(int loanId, UpdateLoanDto dto);
    Task<LoanResponseDto> GetLoanByIdAsync(int loanId);
    Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync();
}
