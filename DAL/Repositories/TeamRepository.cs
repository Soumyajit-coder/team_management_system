using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class TeamRepository : Repository<MTeam>, ITeamRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<MTeam> _dbSet;
        public TeamRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<MTeam>();
        }
        public async Task<String?> GetTeamLeadNameAsync(int userId)
        {
            var teamLeadName = await (from m in _dbContext.MTeams join u in _dbContext.Users on m.TeamLeadId equals u.Id where m.TeamLeadId == userId select u.UserName).FirstOrDefaultAsync();
            return teamLeadName;
        }
        public async Task<List<TeamDetailsDTO>> GetTeamListAsync()
        {
            var teamList = await (from t in _dbContext.MTeams join u in _dbContext.Users on t.TeamLeadId equals u.Id join org in _dbContext.MOrganizations on t.OrgId equals org.Id 
                                  select new TeamDetailsDTO
                                  {
                                      OrgName = org.OrgName,
                                      TeamName = t.TeamName,
                                      Description = t.Description,
                                      TeamLeadName = u.UserName
                                  }).ToListAsync();
            return teamList;
        }
    }
}
