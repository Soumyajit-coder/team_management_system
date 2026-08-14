using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;

namespace team_management_system.DAL.Interfaces
{
    public interface IUserHasRoleRepository : IRepository<UserHasRole>
    {
        Task<UserHasRole> GetAssingedRoleAsync(int user_id, int role_id, bool useNoTracking = false);
        Task<String> GetRoleNameByUserIdAsync(int id);
        Task<int?> GetRoleIdByUserIdAsync(int id);
    }
}
