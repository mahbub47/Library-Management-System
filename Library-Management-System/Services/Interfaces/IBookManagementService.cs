using Library_Management_System.Services.Dtos;

namespace Library_Management_System.Services.Interfaces;

public interface IBookManagementService
{
    Task<BookResponseDto> CreateBookAsync(CreateBookDto dto);
    Task<bool> DeleteBookAsync(int bookId);
    Task<BookResponseDto> UpdateBookAsync(int bookId, UpdateBookDto dto);
    Task<BookResponseDto> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
    Task<IEnumerable<BookResponseDto>> SearchBookAsync(string? title, string? author);
    Task<IEnumerable<LoanResponseDto>> GetAllLoansByBookId(int bookId);
}
