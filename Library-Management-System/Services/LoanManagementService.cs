using Library_Management_System.Entities;
using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;

namespace Library_Management_System.Services;

public class LoanManagementService : ILoanManagementService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;
    public LoanManagementService(
        ILoanRepository loanRepository, 
        IBookRepository bookRepository,
        IMemberRepository memberRepository)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }
    public async Task<bool> CancelLoanAsync(int loanId)
    {
        var loan = await _loanRepository.GetLoanByIdAsync(loanId);
        if(loan == null) return false;
        await _loanRepository.DeleteLoanAsync(loan);
        return true;
    }

    public async Task<LoanResponseDto> CreateLoanAsync(CreateLoanDto dto)
    {
        var book = await _bookRepository.GetBookByIdAsync(dto.BookId);
        var member = await _memberRepository.GetMemberByIdAsync(dto.MemberId);

        if (book == null) return null!;

        if(member == null) return null!;

        if (book.AvailableCopies == 0) return null!;

        if(!member.IsActive) return null!;

        if (member.Loans.FirstOrDefault(l => l.BookId == dto.BookId) != null) return null!;

        var newLoan = new Loan
        {
            MemberId = dto.MemberId,
            BookId = dto.BookId,
            BorrowedAt = DateTime.UtcNow,
            DueDate = dto.DueDate,
        };
        var createdLoan = await _loanRepository.AddLoanAsync(newLoan);
        if (createdLoan == null) return null!;
        book.AvailableCopies -= 1;
        await _bookRepository.UpdateBookAsync(book);
        return new LoanResponseDto
        {
            Id = createdLoan.Id,
            BookName = createdLoan.Book.Title,
            MemberName = createdLoan.Member.FullName,
            BorrowedAt = createdLoan.BorrowedAt,
            DueDate = createdLoan.DueDate,
        };
    }

    public async Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync()
    {
        var loans = await _loanRepository.GetAllLoansAsync();
        var loanResponse = new List<LoanResponseDto>();
        foreach (var loan in loans)
        {
            loanResponse.Add(new LoanResponseDto
            {
                Id=loan.Id,
                BookName=loan.Book.Title,
                MemberName=loan.Member.FullName,
                BorrowedAt=loan.BorrowedAt,
                DueDate=loan.DueDate,
            });
        }
        return loanResponse;
    }

    public async Task<LoanResponseDto> GetLoanByIdAsync(int loanId)
    {
        var loan = await _loanRepository.GetLoanByIdAsync(loanId);
        if(loan == null) return null!;
        return new LoanResponseDto
        {
            Id=loan.Id,
            BookName=loan.Book.Title,
            MemberName=loan.Member.FullName,
            BorrowedAt=loan.BorrowedAt,
            DueDate=loan.DueDate,
        };
    }

    public async Task<LoanResponseDto> UpdateLoanAsync(int laonId, UpdateLoanDto dto)
    {
        var existingLoan = await _loanRepository.GetLoanByIdAsync(laonId);
        if(existingLoan == null) return null!;
        existingLoan.DueDate = dto.DueDate;
        var updatedLoan = await _loanRepository.UpdateLoanAsync(existingLoan);
        return new LoanResponseDto
        {
            Id = updatedLoan.Id,
            BookName = updatedLoan.Book.Title,
            MemberName = updatedLoan.Member.FullName,
            BorrowedAt = updatedLoan.BorrowedAt,
            DueDate = updatedLoan.DueDate,
        };
    }
}
