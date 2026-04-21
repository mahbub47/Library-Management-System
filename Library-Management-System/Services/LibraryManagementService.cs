using Library_Management_System.Entities;
using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;

namespace Library_Management_System.Services;

public class LibraryManagementService : ILibraryManagementService
{
    private readonly ILibraryRepository _libraryRepository;
    public LibraryManagementService(ILibraryRepository libraryRepository) => _libraryRepository = libraryRepository;

    public async Task<BookResponseDto> CreateBookAsync(CreateBookDto dto)
    {
        var newBook = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            AvailableCopies = dto.AvailableCopies,
        };
        var createdBook = await _libraryRepository.AddBookAsync(newBook);
        return new BookResponseDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author,
            ISBN= createdBook.ISBN,
        };
    }

    public async Task<MemberResponseDto> CreateMemberAsync(CreateMemberDto dto)
    {
        var newMember = new Member
        {
            FullName = dto.FullName,
            Email = dto.Email,
            IsActive = true,
        };
        var createdMember = await _libraryRepository.AddMemberAsync(newMember);

        if (createdMember == null)
            return null!;

        return new MemberResponseDto
        {
            Id = createdMember.Id,
            FullName = createdMember.FullName,
            Email = createdMember.Email,
            IsActive = createdMember.IsActive,
        };
    }

    public Task<bool> DeactivateMembershipAsync(int memberId)
    {
        throw new NotImplementedException();
    }

    public Task DecrementBookCopiesAsync(int bookId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBookAsync(int bookId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMemberAsync(int memberId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
    {
        var books = await _libraryRepository.GetAllBooksAsync();

        if (books == null)
            return null!;

        var booksResponse = new List<BookResponseDto>();
        foreach (var book in books)
        {
            booksResponse.Add(new BookResponseDto
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                ISBN = book.ISBN,
            });
        }
        return booksResponse;
    }

    public Task<IEnumerable<MemberResponseDto>> GetAllMemberAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BookResponseDto> GetBookByIdAsync(int bookId)
    {
        var book = await _libraryRepository.GetBookByIdAsync(bookId);

        if (book == null)
            return null!;

        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
        };
    }

    public Task<MemberResponseDto> GetMemberByIdAsync(int memberId)
    {
        throw new NotImplementedException();
    }

    public Task IncrementBookCopiesAsync(int bookId)
    {
        throw new NotImplementedException();
    }

    public async Task<BookResponseDto> UpdateBookAsync(int bookId, UpdateBookDto dto)
    {
        var existedBook = await _libraryRepository.GetBookByIdAsync(bookId);
        if (existedBook == null)
            return null!;
        existedBook.Title = dto.Title;
        existedBook.AvailableCopies = dto.AvailableCopies;

        var updatedBook = await _libraryRepository.UpdateBookAsync(existedBook);
        return new BookResponseDto
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            Author = updatedBook.Author,
            ISBN = updatedBook.ISBN,
        };
    }

    public Task<MemberResponseDto> UpdateMemberAsync(int memberId, UpdateMemberDto dto)
    {
        throw new NotImplementedException();
    }
}
