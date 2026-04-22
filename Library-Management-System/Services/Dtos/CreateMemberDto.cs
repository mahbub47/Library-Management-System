using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Services.Dtos;

public class CreateMemberDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;
}
