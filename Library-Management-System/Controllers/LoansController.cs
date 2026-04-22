using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanManagementService _service;
    public LoansController(ILoanManagementService service) => _service = service;
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateLoanDto dto)
    {
        var loan = await _service.CreateLoanAsync(dto);
        if(loan == null) 
            return BadRequest();
        return CreatedAtAction(nameof(GetById), new { loanId = loan.Id }, loan);
    }

    [HttpGet("{loanId}")]
    public async Task<IActionResult> GetById(int loanId)
    {
        var loan = await _service.GetLoanByIdAsync(loanId);
        if(loan == null)
            return NotFound($"Loan with id {loanId} not found");
        return Ok(loan);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? bookName,
        [FromQuery] string? memberName,
        [FromQuery] int? bookId, 
        [FromQuery] int? memberId, 
        [FromQuery] string? authorName, 
        [FromQuery] string? isbn)
    {
        var loans = await _service.SearchLoanAsync(bookName, memberName, bookId, memberId, authorName, isbn);
        if (loans.IsNullOrEmpty()) return NotFound();
        return Ok(loans);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var loans = await _service.GetAllLoansAsync();
        if (loans.IsNullOrEmpty()) return NotFound("No Loans");
        return Ok(loans);
    }

    [HttpDelete("{loanId}")]
    public async Task<IActionResult> Delete(int loanId)
    {
        var deleted = await _service.CancelLoanAsync(loanId);
        if (!deleted) return BadRequest();
        return NoContent();
    }

    [HttpPatch("{loanId}")]
    public async Task<IActionResult> Update([FromRoute]int loanId, [FromBody]UpdateLoanDto dto)
    {
        var loan = await _service.UpdateLoanAsync(loanId, dto);
        if (loan == null) return BadRequest();
        return Ok(loan);
    }
}
