using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberManagementService _service;
    public MembersController(IMemberManagementService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateMemberDto dto)
    {
        var newMember = await _service.CreateMemberAsync(dto);
        if (newMember == null)
            return BadRequest();
        return CreatedAtAction(nameof(GetById), new { memberId = newMember.Id }, newMember);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await _service.GetAllMemberAsync();
        if(members.IsNullOrEmpty())
            return NotFound("No member found");
        return Ok(members);
    }

    [HttpGet("{memberId}", Name = "GetById")]
    public async Task<IActionResult> GetById([FromRoute]int memberId)
    {
        var member = await _service.GetMemberByIdAsync(memberId);
        if(member == null)
            return NotFound($"Member with id {memberId} not found");
        return Ok(member);
    }

    [HttpPut("{memberId}")]
    public async Task<IActionResult> Update([FromRoute]int memberId, [FromBody]UpdateMemberDto dto)
    {
        var updated = await _service.UpdateMemberAsync(memberId, dto);
        if(updated == null)
            return BadRequest();
        return Ok(updated);
    }

    [HttpPatch("{memberId}/deactivate")]
    public async Task<IActionResult> Deactivate([FromRoute]int memberId)
    {
        var deactivated = await _service.DeactivateMembershipAsync(memberId);
        if (deactivated)
            return Ok($"Membership deactivated for id {memberId}");
        return BadRequest();
    }

    [HttpDelete("{memberId}")]
    public async Task<IActionResult> Delete([FromRoute]int memberId)
    {
        var deleted = await _service.DeleteMemberAsync(memberId);
        if (!deleted) return NotFound("User not found");
        return NoContent();
    }
}
