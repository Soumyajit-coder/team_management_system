using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.DAL.Repositories
{
    public class UserHasRepository : Repository<UserHasRole>, IUserHasRoleRepository
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<UserHasRole> _dbSet;
        public UserHasRepository(teamManagementSystemDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<UserHasRole>();
        }
        public async Task<UserHasRole> GetAssingedRoleAsync(int user_id, int role_id, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbSet.AsNoTracking().Where(u => u.UserId == user_id && u.RoleId == role_id).FirstOrDefaultAsync();
            } else
            {
                return await _dbSet.Where(u => u.UserId == user_id && u.RoleId == role_id).FirstOrDefaultAsync();
            }
        }
        public async Task<String> GetRoleNameByUserIdAsync(int id)
        {
            var userRoleId = await _dbSet.Where(ur => ur.UserId == id).FirstOrDefaultAsync();
            if (userRoleId == null)
            {
                return null;
            }
            var roleName = await _dbContext.Roles.Where(r => r.Id == userRoleId.RoleId).Select(r => r.Slug).FirstOrDefaultAsync();
            return roleName;
        }
        public async Task<int?> GetRoleIdByUserIdAsync(int id)
        {
            var roleId = await _dbSet.Where(ur => ur.UserId == id).Select(ur => ur.RoleId).FirstOrDefaultAsync();
            if (roleId == null)
            {
                return null;
            }
            return roleId;
        }
    }
}
