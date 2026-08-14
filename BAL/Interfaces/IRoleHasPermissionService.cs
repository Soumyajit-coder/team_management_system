using team_management_system.DAL.Entities;

namespace team_management_system.BAL.Interfaces
{
    public interface IRoleHasPermissionService
    {
        Task<List<String>> GetAssignPermissionNameAsync(int roleId);
        //Task<MRoleHasPermission> GetAssignPermissionNameAsync(int roleId);
    }
}
