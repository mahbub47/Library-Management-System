using Library_Management_System.Services.Dtos;

namespace Library_Management_System.Services.Interfaces;

public interface ILibraryManagementService
{
    Task<BookResponseDto> CreateBookAsync(CreateBookDto dto);
    Task DecrementBookCopiesAsync(int bookId);
    Task IncrementBookCopiesAsync(int bookId);
    Task<bool> DeleteBookAsync(int bookId);
    Task<BookResponseDto> UpdateBookAsync(int bookId, UpdateBookDto dto);
    Task<BookResponseDto> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();


    Task<MemberResponseDto> CreateMemberAsync(CreateMemberDto dto);
    Task<bool> DeactivateMembershipAsync(int  memberId);
    Task<bool> DeleteMemberAsync(int memberId);
    Task<MemberResponseDto> UpdateMemberAsync(int memberId, UpdateMemberDto dto);
    Task<MemberResponseDto> GetMemberByIdAsync(int memberId);
    Task<IEnumerable<MemberResponseDto>> GetAllMemberAsync();
}

