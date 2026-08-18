using team_management_system.DAL.Entities;
using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface IPermissionService
    {
        Task<List<MPermission>> GetAllPermissionAsync();
        Task<long> CreatePermissionAsync(PermissionDTO dto);
        Task<MPermission> GetPermissionByConditionAsync(string condition);
    }
}
