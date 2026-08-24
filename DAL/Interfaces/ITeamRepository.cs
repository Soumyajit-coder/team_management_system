using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DTO;

namespace team_management_system.DAL.Interfaces
{
    public interface ITeamRepository : IRepository<MTeam>
    {
        Task<String> GetTeamLeadNameAsync(int userId);
        Task<List<TeamDetailsDTO>> GetTeamListAsync();
    }
}
