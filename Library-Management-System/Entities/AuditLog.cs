namespace Library_Management_System.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public DateTime OccuredAt { get; set; }
}
