using Library_Management_System.Entities;

namespace Library_Management_System.Services.Interfaces;

public interface ILibraryRepository
{
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int bookId);

    Task<Member> AddMemberAsync(Member member);
    Task<Member> UpdateMemberAsync(Member member);
    Task<Member> GetMemberByIdAsync(int memberId);
    Task<IEnumerable<Member>> GetAllMembersAsync();
}
