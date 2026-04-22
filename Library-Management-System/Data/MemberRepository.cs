using Library_Management_System.Entities;
using Library_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryManagementDBContext _context;
    public MemberRepository(LibraryManagementDBContext context) => _context = context;
    public async Task<Member> AddMemberAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task DeleteMemberAsync(Member member)
    {
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansByMember(int memberId)
    {
        return await _context.Loans.Include(l => l.Book).Include(l => l.Member).Where(l => l.MemberId == memberId).ToListAsync();
    }

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return await _context.Members.Include(m => m.Loans).ToListAsync();
    }

    public async Task<Member> GetMemberByIdAsync(int memberId)
    {
        return await _context.Members.Include(m => m.Loans).Where(m => m.Id == memberId).FirstOrDefaultAsync();
    }

    public async Task<Member> UpdateMemberAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return member;
    }
}
