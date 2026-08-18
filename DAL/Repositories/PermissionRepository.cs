using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.DAL.Repositories
{
    public class PermissionRepository : Repository<MPermission>, IPermissionRepository
    {
        public PermissionRepository(teamManagementSystemDBContext dbContext) : base(dbContext)
        {
        }
    }
}
