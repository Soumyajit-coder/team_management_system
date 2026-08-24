using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamDetailsDTO>> GetTeamsListAsync();
        Task<long> CreateTeamDetails(TeamDTO dto);
        Task<String> TeamLeadNameAsync(int userId);
    }
}
