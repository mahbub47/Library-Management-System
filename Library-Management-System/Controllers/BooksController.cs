using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookManagementService _service;
    public BooksController(IBookManagementService service) => _service = service;
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateBookDto dto)
    {
        var createdBook = await _service.CreateBookAsync(dto);
        return CreatedAtAction(nameof(GetById), new { bookId = createdBook.Id }, createdBook);
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetById([FromRoute]int bookId)
    {
        var book = await _service.GetBookByIdAsync(bookId);
        if(book == null)
            return NotFound($"Book with id {bookId} Not Found");
        return Ok(book);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author)
    {
        var books = await _service.SearchBookAsync(title, author);
        if (books.IsNullOrEmpty()) return NotFound();
        return Ok(books);
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

    [HttpGet("{bookId}/loans")]
    public async Task<IActionResult> GetAllLoans([FromRoute] int bookId)
    {
        var loans = await _service.GetAllLoansByBookId(bookId);
        if (loans.IsNullOrEmpty()) return NotFound();
        return Ok(loans);
    }
}
