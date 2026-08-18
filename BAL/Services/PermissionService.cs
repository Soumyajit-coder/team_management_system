using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public async Task<List<MPermission>> GetAllPermissionAsync()
        {
            var permissionList = await _permissionRepository.GetAllAsync();
            return permissionList.ToList();
        }
        public async Task<long> CreatePermissionAsync(PermissionDTO dto)
        {
            MPermission createPermission = _mapper.Map<MPermission>(dto);
            if (string.IsNullOrEmpty(createPermission.Slug) && !string.IsNullOrEmpty(createPermission.PermissionName))
            {
                createPermission.Slug = createPermission.PermissionName.Trim().ToLower().Replace("_", ".");                
            }
            await _permissionRepository.CreateAsync(createPermission);
            return createPermission.Id;
        }
        public async Task<MPermission> GetPermissionByConditionAsync(string condition)
        {
            var permissionName = await _permissionRepository.GetDetailsAsync(p => p.PermissionName == condition);
            return permissionName;
        }
    }
}
