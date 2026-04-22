using Microsoft.AspNetCore.Http.Timeouts;
using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Services.Dtos;

public class CreateLoanDto
{
    [Required]
    public int? BookId { get; set; }

    [Required]
    public int? MemberId { get; set; }

    [Required]
    public DateTime DueDate { get; set; }
}
