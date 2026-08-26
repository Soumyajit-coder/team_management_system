using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DTO;

namespace team_management_system.DAL.Interfaces
{
    public interface ITeamMemberRepository : IRepository<TeamMember>
    {
        Task<List<TeamMemberDetailsDTO>> GetTeamMemberListAsync();
        Task<TeamMemberDetailsDTO> GetMemberByUserIdAsync(int userId);
        Task<List<TeamMemberDetailsDTO>> GetTeamMembersByTeamNameAsync(string teamName);
        Task<bool> GetTeamDetailsByIdAsync(int teamId, int userId);
    }
}
