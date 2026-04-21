using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly ILibraryManagementService _service;
    public MembersController(ILibraryManagementService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateMemberDto dto)
    {
        var newMember = await _service.CreateMemberAsync(dto);
        if (newMember == null)
            return BadRequest();
        //return CreatedAtAction(nameof(GetById), new { Id = newMember.Id }, newMember);
        return Created();
    }

    [HttpGet("{memberId}")]
    public async Task<IActionResult> GetById([FromRoute]int memberId)
    {
        var member = await _service.GetMemberByIdAsync(memberId);
        if(member == null)
            return NotFound($"Member with id {memberId} not found");
        return Ok(member);
    }
}
