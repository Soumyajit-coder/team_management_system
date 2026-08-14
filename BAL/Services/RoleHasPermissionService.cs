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
            return permissionName;
        }
    }
}
