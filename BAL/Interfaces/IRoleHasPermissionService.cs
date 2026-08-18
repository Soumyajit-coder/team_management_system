using team_management_system.DAL.Entities;

namespace team_management_system.BAL.Interfaces
{
    public interface IRoleHasPermissionService
    {
        Task<List<String>> GetAssignPermissionNameAsync(int roleId);
        Task<bool> AssignedPermissionByRoleAsync(int roleId, int permissionId);
        Task<MRoleHasPermission> GetRolePermissionByIdAsync(int roleId, int permissionId);
    }
}
