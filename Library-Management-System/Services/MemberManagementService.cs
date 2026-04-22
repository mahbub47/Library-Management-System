using Library_Management_System.Entities;
using Library_Management_System.Services.Dtos;
using Library_Management_System.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.Services;

public class MemberManagementService : IMemberManagementService
{
    private readonly IMemberRepository _memberRepository;
    public MemberManagementService(IMemberRepository memberRepository) => _memberRepository = memberRepository;
    public async Task<MemberResponseDto> CreateMemberAsync(CreateMemberDto dto)
    {
        var newMember = new Member
        {
            FullName = dto.FullName,
            Email = dto.Email,
            IsActive = true
        };
        var createdMember = await _memberRepository.AddMemberAsync(newMember);
        if (createdMember == null)
            return null!;
        return new MemberResponseDto
        {
            Id = createdMember.Id,
            FullName = createdMember.FullName,
            Email = createdMember.Email,
            IsActive = createdMember.IsActive
        };
    }

    public async Task<bool> DeactivateMembershipAsync(int memberId)
    {
        var existingMember = await _memberRepository.GetMemberByIdAsync(memberId);
        if(existingMember == null) return false;
        if(!existingMember.IsActive) return false;
        existingMember.IsActive = false;
        await _memberRepository.UpdateMemberAsync(existingMember);
        return true;
    }

    public async Task<bool> DeleteMemberAsync(int memberId)
    {
        var exisitngMember = await _memberRepository.GetMemberByIdAsync(memberId);
        if (exisitngMember == null) return false;
        await _memberRepository.DeleteMemberAsync(exisitngMember);
        return true;
    }

    public async Task<IEnumerable<MemberResponseDto>> GetAllMemberAsync()
    {
        var members = await _memberRepository.GetAllMembersAsync();
        var responseMembers = new List<MemberResponseDto>();
        foreach(var member in members)
        {
            responseMembers.Add(new MemberResponseDto
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                IsActive = member.IsActive,
            });
        }
        return responseMembers;
    }

    public async Task<MemberResponseDto> GetMemberByIdAsync(int memberId)
    {
        var member = await _memberRepository.GetMemberByIdAsync(memberId);
        if (member == null) return null!;
        return new MemberResponseDto
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            IsActive = member.IsActive,
        };
    }

    public async Task<MemberResponseDto> UpdateMemberAsync(int memberId, UpdateMemberDto dto)
    {
        var existingMember = await _memberRepository.GetMemberByIdAsync(memberId);

        if(existingMember == null) return null!;

        existingMember.FullName = dto.FullName ?? existingMember.FullName;
        existingMember.Email = dto.Email ?? existingMember.Email;

        var updatedMember = await _memberRepository.UpdateMemberAsync(existingMember);

        return new MemberResponseDto
        {
            Id = updatedMember.Id,
            FullName = updatedMember.FullName,
            Email = updatedMember.Email,
            IsActive = updatedMember.IsActive,
        };
    }
}
