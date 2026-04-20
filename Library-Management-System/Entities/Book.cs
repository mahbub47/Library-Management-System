namespace Library_Management_System.Entities;

public class Book
{
    public int Id { get; init; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public int AvailableCopies { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
