using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Services.Dtos;

public class CreateBookDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string ISBN { get; set; } = string.Empty;

    [Required]
    public int? AvailableCopies { get; set; }
}
