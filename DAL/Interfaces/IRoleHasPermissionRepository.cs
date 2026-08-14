using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;

namespace team_management_system.DAL.Interfaces
{
    public interface IRoleHasPermissionRepository : IRepository<MRoleHasPermission>
    {
        Task<List<String>> GetPermissionByRoleIdAsync(int id);
    }
}
