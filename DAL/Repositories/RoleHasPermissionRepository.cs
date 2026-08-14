using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.DAL.Repositories
{
    public class RoleHasPermissionRepository : Repository<MRoleHasPermission>, IRoleHasPermissionRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<MRoleHasPermission> _dbSet;
        public RoleHasPermissionRepository(teamManagementSystemDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<MRoleHasPermission>();
        }
        public async Task<List<String>> GetPermissionByRoleIdAsync(int id)
        {
            var rolePermissionId = await _dbSet.Where(rp => rp.RoleId == id).FirstOrDefaultAsync();
            if (rolePermissionId == null)
            {
                return null;
            }
            var permissionName = await (from rhp in _dbContext.MRoleHasPermissions join p in _dbContext.MPermissions on rhp.PermissionId equals p.Id where rhp.RoleId == id select p.Slug).ToListAsync();
            return permissionName;
        }
    }
}
