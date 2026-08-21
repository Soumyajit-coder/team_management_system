using team_management_system.DAL.Entities;
using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface IOrganizationMgmtService
    {
        Task<List<MOrganization>> GetOrganizationListAsync();
        Task<MOrganization> GetOrganizationById(int id);
        Task<long> CreateOrganizationAsync(OrganizationDTO dto);
        //Task<String> GetOrganizationByConditionAsync(string condition);
    }
}
