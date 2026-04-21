using Library_Management_System.Entities;
using Library_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data;

public class LibraryRepository : ILibraryRepository
{
    private readonly LibraryManagementDBContext _context;
    public LibraryRepository(LibraryManagementDBContext context) => _context = context;
    public async Task<Book> AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task<Member> AddMemberAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Book> GetBookByIdAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return null!;
        return book;
    }

    public Task<Member> GetMemberByIdAsync(int memberId)
    {
        throw new NotImplementedException();
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        return book;
    }

    public Task<Member> UpdateMemberAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Member> UpdateMemberAsync(Member member)
    {
        throw new NotImplementedException();
    }
}
