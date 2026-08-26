using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface ITeamMemberService
    {
        Task<List<TeamMemberDetailsDTO>> GetTeamMembersListAsync();
        Task<TeamMemberDetailsDTO> GetMemberByUserIdAsync(int userId);
        Task<List<TeamMemberDetailsDTO>> GetTeamMembersDetailsAsync(string teamId);
        Task<long> CreateTeamMembersAsync(TeamMemberDTO dto);
        Task<bool> ExistInTeamMemberAsync(int userId, int TeamId);
    }
}
