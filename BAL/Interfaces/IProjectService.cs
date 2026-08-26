using team_management_system.DAL.Entities;
using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface IProjectService
    {
        Task<List<MProject>> GetProjectListAsync();
        Task<long> CreateProjectAsync(ProjectDTO dto);
        Task<MProject> GetProjectDetailsByIAsync(int p_id);
        Task<List<ProjectDetailsDTO>> GetProjectDetailsByOrgNameAsync(string org_name);
        Task<String> GetProjectDetailsByName(string p_name);
    }
}
