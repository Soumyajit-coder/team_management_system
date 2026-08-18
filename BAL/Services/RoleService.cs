using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<List<Role>> GetAllRoleAsync()
        {
            var roleList = await _roleRepository.GetAllAsync();
            return roleList;
        }
        public async Task<Role> GetRoleDetailsAsync(int id)
        {
            var roleDetails = await _roleRepository.GetDetailsByIdAsync(id);
            return roleDetails;
        }
        public async Task<long> CreateRoleAsync(RoleDTO dto)
        {
            Role createRoleDetails = _mapper.Map<Role>(dto);
            if (string.IsNullOrEmpty(createRoleDetails.Slug) && !string.IsNullOrEmpty(createRoleDetails.RoleName))
            {
                createRoleDetails.Slug = createRoleDetails.RoleName.Trim().ToLower().Replace(" ", "_");
            }
            await _roleRepository.CreateAsync(createRoleDetails);
            return createRoleDetails.Id;
        }
        public async Task<Role> GetRoleByConditionAsync(string roleName)
        {
            var roleDetails = await _roleRepository.GetDetailsAsync(r => r.RoleName == roleName);
            return roleDetails;
        }
    }
}
