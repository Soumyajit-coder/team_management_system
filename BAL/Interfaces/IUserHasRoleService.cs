using team_management_system.DAL.Entities;

namespace team_management_system.BAL.Interfaces
{
    public interface IUserHasRoleService
    {
        Task<bool> AssignRoleAsync(int userId, int roleId);
        Task<string> GetAssignRoleName(int userId);
        Task<int> GetAssignedRoleId(int userId);
    }
}
