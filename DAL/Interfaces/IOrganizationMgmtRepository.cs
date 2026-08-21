using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;

namespace team_management_system.DAL.Interfaces
{
    public interface IOrganizationMgmtRepository : IRepository<MOrganization>
    {
        Task<MOrganization> GetDetailsByConditionAsync(string condition);
    }
}
