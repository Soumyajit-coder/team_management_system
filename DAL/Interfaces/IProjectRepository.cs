using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DTO;

namespace team_management_system.DAL.Interfaces
{
    public interface IProjectRepository : IRepository<MProject>
    {
        Task<List<ProjectDetailsDTO>> GetProjectDetailsByOrgId(int orgId);
        Task<List<ProjectDetailsDTO>> GetProjectDetailsByOrgName(string orgName);
    }
}
