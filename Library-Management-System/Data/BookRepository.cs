using Library_Management_System.Entities;
using Library_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Data;

public class BookRepository : IBookRepository
{
    private readonly LibraryManagementDBContext _context;
    public BookRepository(LibraryManagementDBContext context) => _context = context;
    public async Task<Book> AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task DeleteBookAsync(Book book)
    {
        _context.Remove(book);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return null!;
        return book;
    }

    public async Task<IEnumerable<Book>> SearchBookAsync(string? title, string? author)
    {
        var query = _context.Books.AsQueryable();

        if(!title.IsNullOrEmpty())
            query = query.Where(b => b.Title == title);

        if (!author.IsNullOrEmpty())
            query = query.Where(b => b.Author == author);

        return await query.ToListAsync();
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
}
