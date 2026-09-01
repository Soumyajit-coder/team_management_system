using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class ProjectMemberRepository : Repository<ProjectMember>, IProjectMemberRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<ProjectMember> _dbSet;
        public ProjectMemberRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProjectMember>();
        }
        public async Task<List<ProjectMemberDetailsDTO>> GetProjectDetailsList()
        {
            var projectList = await (from pm in _dbContext.ProjectMembers join p in _dbContext.MProjects on pm.ProjectId equals p.Id join u in _dbContext.Users on pm.UserId equals u.Id
                                     select new ProjectMemberDetailsDTO
                                     {
                                         ProjectName = p.ProjectName,
                                         UserName = u.UserName,
                                         Role = pm.Role,
                                         JoinedAt = pm.JoinedAt
                                     }
                                     ).ToListAsync();
            return projectList;
        }
        public async Task<List<ProjectMemberDetailsDTO>> SerachByProjectName(string p_name)
        {
            var projectDetailsList = await (from pm in _dbContext.ProjectMembers
                                            join p in _dbContext.MProjects on pm.ProjectId equals p.Id
                                            join u in _dbContext.Users on pm.UserId equals u.Id
                                            where p.ProjectName == p_name
                                            select new ProjectMemberDetailsDTO
                                            {
                                                ProjectName = p.ProjectName,
                                                UserName = u.UserName,
                                                Role = pm.Role,
                                                JoinedAt = pm.JoinedAt
                                            }
                                     ).ToListAsync();
            return projectDetailsList;
        }
        public async Task<List<ProjectMemberDetailsDTO>> SerachByProjectId(int p_id)
        {
            var projectDetailsList = await (from pm in _dbContext.ProjectMembers
                                            join p in _dbContext.MProjects on pm.ProjectId equals p.Id
                                            join u in _dbContext.Users on pm.UserId equals u.Id
                                            where pm.ProjectId == p_id
                                            select new ProjectMemberDetailsDTO
                                            {
                                                ProjectName = p.ProjectName,
                                                UserName = u.UserName,
                                                Role = pm.Role,
                                                JoinedAt = pm.JoinedAt
                                            }
                                     ).ToListAsync();
            return projectDetailsList;
        }
        public async Task<bool> MembersActiveDeactiveAsync(long id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            if (user.IsActive == 0)
            {
                int rowsAffected = await _dbSet.Where(u => u.Id == id).ExecuteUpdateAsync(s => s.SetProperty(u => u.IsActive, 1));
                return true;
            } else
            {
                int rowsAffected = await _dbSet.Where(u => u.Id == id).ExecuteUpdateAsync(s => s.SetProperty(u => u.IsActive, 0));
                return true;
            }
        }
    }
}
