using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.DAL.Repositories
{
    public class TaskMgmtRepository : Repository<TaskMgmt>, ITaskMgmtRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<TaskMgmt> _dbSet;
        public TaskMgmtRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TaskMgmt>();
        }

        public async Task<List<TaskMgmtDetailsDTO>> GetTaskListAsync()
        {
            var taskList = await (from t in _dbContext.TaskMgmts join org in _dbContext.MOrganizations on t.TeamId equals org.Id
                                  join proj in _dbContext.MProjects on t.ProjectId equals proj.Id
                                  join tm in _dbContext.MTeams on t.TeamId equals tm.Id
                                  select new TaskMgmtDetailsDTO
                                  {
                                      OrganizationName = org.OrgName,
                                      ProjectName = proj.ProjectName,
                                      TeamName = tm.TeamName,
                                      Title = t.Title,
                                      Description = t.Description,
                                      Priority = t.Priority == 1 ? "Low" : t.Priority == 2 ? "Medium" : "High",
                                      Status = t.Status == 1 ? "To Do" : t.Status == 2 ? "In Progress" : t.Status == 3 ? "Completed" : "On Hold"
                                  }).ToListAsync();
            return taskList;
        }
    }
}
