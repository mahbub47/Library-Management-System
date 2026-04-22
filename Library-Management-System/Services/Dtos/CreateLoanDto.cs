namespace Library_Management_System.Services.Dtos;

public class CreateLoanDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime DueDate { get; set; }
}
