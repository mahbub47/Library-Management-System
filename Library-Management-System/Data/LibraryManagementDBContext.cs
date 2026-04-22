using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data;

public class LibraryManagementDBContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public LibraryManagementDBContext(DbContextOptions<LibraryManagementDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Title).HasMaxLength(150).IsRequired(true);

            entity.Property(b => b.ISBN).IsRequired(true);

            entity.HasIndex(b => b.ISBN).IsUnique(true);

            entity.HasMany<Loan>(b => b.Loans).WithOne(l => l.Book).HasForeignKey(l => l.BookId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(m => m.IsActive).HasDefaultValue(true);

            entity.Property(m => m.Email).IsRequired(true).HasMaxLength(200);

            entity.HasIndex(m => m.Email).IsUnique(true);

            entity.HasQueryFilter(m => m.IsActive);

            entity.HasMany<Loan>(m => m.Loans).WithOne(l => l.Member).HasForeignKey(l => l.MemberId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasOne<Member>(l => l.Member).WithMany(m => m.Loans).HasForeignKey(l => l.MemberId);

            entity.HasOne<Book>(l => l.Book).WithMany(b => b.Loans).HasForeignKey(l => l.BookId);
        });
    }
}
