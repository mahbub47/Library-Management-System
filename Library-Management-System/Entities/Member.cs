namespace Library_Management_System.Entities;

public class Member
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
