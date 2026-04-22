using Library_Management_System.Entities;

namespace Library_Management_System.Services.Interfaces;

public interface IBookRepository
{
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int bookId);
    Task DeleteBookAsync(Book book);
}
