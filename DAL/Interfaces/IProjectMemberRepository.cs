using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DTO;

namespace team_management_system.DAL.Interfaces
{
    public interface IProjectMemberRepository : IRepository<ProjectMember>
    {
        Task<List<ProjectMemberDetailsDTO>> GetProjectDetailsList();
        Task<List<ProjectMemberDetailsDTO>> SerachByProjectName(string p_name);
        Task<List<ProjectMemberDetailsDTO>> SerachByProjectId(int p_id);
        Task<bool> MembersActiveDeactiveAsync(long id);
    }
}
