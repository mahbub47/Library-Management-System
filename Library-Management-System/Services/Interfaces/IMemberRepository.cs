using Library_Management_System.Entities;

namespace Library_Management_System.Services.Interfaces;

public interface IMemberRepository
{
    Task<Member> AddMemberAsync(Member member);
    Task<Member> UpdateMemberAsync(Member member);
    Task<Member> GetMemberByIdAsync(int memberId);
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task DeleteMemberAsync(Member member);
}
