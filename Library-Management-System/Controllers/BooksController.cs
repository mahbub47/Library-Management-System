using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILibraryManagementService _service;
    public BooksController(ILibraryManagementService service) => _service = service;
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateBookDto dto)
    {
        var createdBook = await _service.CreateBookAsync(dto);
        return CreatedAtAction(nameof(GetById), new { Id = createdBook.Id }, createdBook);
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetById([FromRoute]int bookId)
    {
        var book = await _service.GetBookByIdAsync(bookId);
        if(book == null)
            return NotFound($"Book with id {bookId} Not Found");
        return Ok(book);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _service.GetAllBooksAsync();
        if (books.IsNullOrEmpty())
            return NotFound("No books found");
        return Ok(books);
    }

    [HttpPut("{bookId}")]
    public async Task<IActionResult> UpdateBook([FromRoute]int bookId, [FromBody]UpdateBookDto dto)
    {
        var book = await _service.UpdateBookAsync(bookId, dto);

        if (book == null)
            return NotFound($"No book found with the id {bookId}");

        return Ok(book);
    }
}
