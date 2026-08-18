using team_management_system.DAL.Entities;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.BAL.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRoleAsync();
        Task<Role> GetRoleDetailsAsync(int id);
        Task<Role> GetRoleByConditionAsync(string roleName);
        Task<long> CreateRoleAsync(RoleDTO dto);
    }
}
