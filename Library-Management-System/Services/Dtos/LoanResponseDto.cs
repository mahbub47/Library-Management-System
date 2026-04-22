namespace Library_Management_System.Services.Dtos;

public class LoanResponseDto
{
    public int Id { get; set; }
    public string BookName { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
}
