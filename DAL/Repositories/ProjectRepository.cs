using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class ProjectRepository : Repository<MProject>, IProjectRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<MProject> _dbSet;
        public ProjectRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<MProject>();
        }
        public async Task<List<ProjectDetailsDTO>> GetProjectDetailsByOrgId(int orgId)
        {
            var projectDetails = await (from p in _dbContext.MProjects join org in _dbContext.MOrganizations on p.OrgId equals org.Id where p.OrgId == orgId
                select new ProjectDetailsDTO
                {
                    OrgName = org.OrgName,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    FromDate = p.FromDate,
                    ToDate = p.ToDate,
                    Deadline = p.Deadline
                }).ToListAsync();
            return projectDetails;
        }
        public async Task<List<ProjectDetailsDTO>> GetProjectDetailsByOrgName(string orgName)
        {
            var projectDetails = await (from p in _dbContext.MProjects
                                        join org in _dbContext.MOrganizations on p.OrgId equals org.Id
                                        where org.OrgName == orgName
                                        select new ProjectDetailsDTO
                                        {
                                            OrgName = org.OrgName,
                                            ProjectName = p.ProjectName,
                                            Description = p.Description,
                                            FromDate = p.FromDate,
                                            ToDate = p.ToDate,
                                            Deadline = p.Deadline
                                        }).ToListAsync();
            return projectDetails;
        }
    }
}
