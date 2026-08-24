using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DTO;

namespace team_management_system.DAL.Interfaces
{
    public interface IOrganizationMembersRepository : IRepository<OrganizationMember>
    {
        Task<List<OrganizationMembersDetailsDTO>> GetOrgMemeberListAsync();
        Task<String> GetNameByIdAsync(int userId);
        Task<List<String>> GetOrgNameByIdAsync(int orgId);
    }
}
