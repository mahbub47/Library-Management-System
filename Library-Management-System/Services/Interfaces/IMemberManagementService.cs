using Library_Management_System.Services.Dtos;

namespace Library_Management_System.Services.Interfaces;

public interface IMemberManagementService
{
    Task<MemberResponseDto> CreateMemberAsync(CreateMemberDto dto);
    Task<bool> DeactivateMembershipAsync(int memberId);
    Task<bool> DeleteMemberAsync(int memberId);
    Task<MemberResponseDto> UpdateMemberAsync(int memberId, UpdateMemberDto dto);
    Task<MemberResponseDto> GetMemberByIdAsync(int memberId);
    Task<IEnumerable<MemberResponseDto>> GetAllMemberAsync();
}
