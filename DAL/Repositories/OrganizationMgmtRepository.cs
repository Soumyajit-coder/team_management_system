using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.DAL.Repositories
{
    public class OrganizationMgmtRepository : Repository<MOrganization>, IOrganizationMgmtRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<MOrganization> _dbSet;
        public OrganizationMgmtRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<MOrganization>();
        }

        public async Task<MOrganization> GetDetailsByConditionAsync(string condition)
        {
            var OrgName = await _dbSet.Where(x => x.OrgName == condition).FirstOrDefaultAsync();
            return OrgName;
        }
    }
}
