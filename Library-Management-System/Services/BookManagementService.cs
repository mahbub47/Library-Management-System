using Library_Management_System.Data;
using Library_Management_System.Entities;
using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Services;

public class BookManagementService : IBookManagementService
{
    private readonly IBookRepository _bookRepository;
    public BookManagementService(IBookRepository libraryRepository) => _bookRepository = libraryRepository;
    public async Task<BookResponseDto> CreateBookAsync(CreateBookDto dto)
    {
        var newBook = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            AvailableCopies = dto.AvailableCopies!.Value
        };
        var createdBook = await _bookRepository.AddBookAsync(newBook);
        return new BookResponseDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author,
            ISBN = createdBook.ISBN,
        };
    }

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId);
        if(book == null)
            return false;
        await _bookRepository.DeleteBookAsync(book);
        return true;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllBooksAsync();

        if (books.IsNullOrEmpty())
            return null!;

        var responseBooks = new List<BookResponseDto>();
        foreach (var book in books)
        {
            responseBooks.Add(new BookResponseDto
            {
                Id = book.Id,
                Title= book.Title,
                Author= book.Author,
                ISBN= book.ISBN,
            });
        }
        return responseBooks;
    }

    public async Task<BookResponseDto> GetBookByIdAsync(int bookId)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId);
        if(book == null)
            return null!;
        return new BookResponseDto
        {
            Id=book.Id,
            Title=book.Title,
            Author=book.Author,
            ISBN=book.ISBN,
        };
    }

    public async Task<BookResponseDto> UpdateBookAsync(int bookId, UpdateBookDto dto)
    {
        var existingBook = await _bookRepository.GetBookByIdAsync(bookId);
        if (existingBook == null)
            return null!;
        existingBook.Title = dto.Title ?? existingBook.Title;
        existingBook.AvailableCopies = dto.AvailableCopies ?? existingBook.AvailableCopies;
        var updatedBook = await _bookRepository.UpdateBookAsync(existingBook);
        return new BookResponseDto
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            Author = updatedBook.Author,
            ISBN = updatedBook.ISBN,
        };
    }
}
