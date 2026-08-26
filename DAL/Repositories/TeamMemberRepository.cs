using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<TeamMember> _dbSet;
        public TeamMemberRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TeamMember>();
        }
        public async Task<List<TeamMemberDetailsDTO>> GetTeamMemberListAsync()
        {
            var memberList = await (from tm in _dbContext.TeamMembers join t in _dbContext.MTeams on tm.TeamId equals t.Id join u in _dbContext.Users on tm.UserId equals u.Id
                                    select new TeamMemberDetailsDTO
                                    {
                                        TeamName = t.TeamName,
                                        UserName = u.UserName,
                                        Doj = tm.Doj,
                                        IsActive = tm.IsActive == 1 ? "Active" : "Inactive"
                                    }
                                   ).ToListAsync();
            return memberList;
        }
        public async Task<TeamMemberDetailsDTO> GetMemberByUserIdAsync(int userId)
        {
            var memberAssignedTeam = await (from tm in _dbContext.TeamMembers
                                            join t in _dbContext.MTeams on tm.TeamId equals t.Id
                                            join u in _dbContext.Users on tm.UserId equals u.Id
                                            where tm.UserId == userId
                                            select new TeamMemberDetailsDTO
                                            {
                                                TeamName = t.TeamName,
                                                UserName = u.UserName,
                                                Doj = tm.Doj,
                                                IsActive = tm.IsActive == 1 ? "Active" : "Inactive"
                                            }).FirstOrDefaultAsync();
            return memberAssignedTeam;
        }
        public async Task<List<TeamMemberDetailsDTO>> GetTeamMembersByTeamNameAsync(string teamName)
        {
            var teamDetails = await (from tm in _dbContext.TeamMembers
                                     join t in _dbContext.MTeams on tm.TeamId equals t.Id
                                     join u in _dbContext.Users on tm.UserId equals u.Id
                                     where t.TeamName == teamName
                                     select new TeamMemberDetailsDTO
                                     {
                                         TeamName = t.TeamName,
                                         UserName = u.UserName,
                                         Doj = tm.Doj,
                                         IsActive = tm.IsActive == 1 ? "Active" : "Inactive"
                                     }).ToListAsync();
            return teamDetails;
        }
        public async Task<bool> GetTeamDetailsByIdAsync(int teamId, int userId)
        {
            bool isExist = await _dbContext.TeamMembers.AnyAsync(tm => tm.UserId == userId && tm.TeamId == teamId);
            return isExist;
        }
    }
}
