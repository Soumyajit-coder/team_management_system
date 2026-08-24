using team_management_system.DAL.Entities;
using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface IOrganizationMemberService
    {
        Task<List<OrganizationMembersDetailsDTO>> GetOrganizationMembersListAsync();
        Task<String> GetOrgUserNameById(int userId);
        Task<long> CreateMembersForOrgAsync(OrganizationMembersDTO dto);
        //Task<String> GetOrganizationNameByIdAsync(string condition);
    }
}
