using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface IProjectMemberService
    {
        Task<List<ProjectMemberDetailsDTO>> GetProjectMemeberList();
        Task<long> CreateProjectMember(ProjectMemberDTO dto);
        Task<List<ProjectMemberDetailsDTO>> SearchProjectMemeberListByProjectName(string p_name);
    }
}
