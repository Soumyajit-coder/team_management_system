using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.BAL.Services
{
    public class RoleHasPermissionService : IRoleHasPermissionService
    {
        private readonly IRoleHasPermissionRepository _roleHasPermissionRepository;
        private IMapper _mapper;
        public RoleHasPermissionService(IRoleHasPermissionRepository roleHasPermissionRepository, IMapper mapper)
        {
            _roleHasPermissionRepository = roleHasPermissionRepository;
            _mapper = mapper;
        }

        public async Task<List<String>> GetAssignPermissionNameAsync(int roleId)
        {
            var permissionName = await _roleHasPermissionRepository.GetPermissionByRoleIdAsync(roleId);
            return permissionName.ToList();
        }
        public async Task<bool> AssignedPermissionByRoleAsync(int roleId, int permissionId)
        { 
            var existingPermission = await _roleHasPermissionRepository.GetAssignedPermissionByRoleAsync(roleId, permissionId);
            if (existingPermission != null)
            {
                return true;
            }
            //var getLastId = await _roleHasPermissionRepository.GetLastIdAsync(x => x.Id);
            MRoleHasPermission roleHasPermission = new MRoleHasPermission
            {
                //Id = getLastId,
                RoleId = roleId,
                PermissionId = permissionId,
                CreatedAt = DateTime.UtcNow
            };
            await _roleHasPermissionRepository.CreateAsync(roleHasPermission); // Save into the table
            return true;
        }
        public async Task<MRoleHasPermission> GetRolePermissionByIdAsync(int roleId, int permissionId)
        {
            var rolePermission = await _roleHasPermissionRepository.GetAssignedPermissionByRoleAsync(roleId, permissionId);
            return rolePermission;
        }
    }
}
