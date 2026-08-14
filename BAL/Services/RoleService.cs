using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

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
    }
}
