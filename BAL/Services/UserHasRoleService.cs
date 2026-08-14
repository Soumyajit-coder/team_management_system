using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;

namespace team_management_system.BAL.Services
{
    public class UserHasRoleService : IUserHasRoleService
    {
        private readonly IUserHasRoleRepository _userHasRoleRepository;
        private IMapper _mapper;
        public UserHasRoleService(IUserHasRoleRepository userHasRoleRepository, IMapper mapper)
        {
            _userHasRoleRepository = userHasRoleRepository;
            _mapper = mapper;
        }
        public async Task<bool> AssignRoleAsync(int userId, int roleId)
        {
            var existingRole = await _userHasRoleRepository.GetAssingedRoleAsync(userId, roleId);
            if (existingRole != null)
            {
                return true;
            }
            // Insert a new row
            UserHasRole userHasRole = new UserHasRole
            {
                UserId = userId,
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            };
            await _userHasRoleRepository.CreateAsync(userHasRole); // Save into the table
            return true;
        }
        public async Task<string> GetAssignRoleName(int userId)
        {
            var roleName = await _userHasRoleRepository.GetRoleNameByUserIdAsync(userId);
            return roleName;
        }

        public async Task<int> GetAssignedRoleId(int userId)
        {
            var roleId = await _userHasRoleRepository.GetRoleIdByUserIdAsync(userId);
            if (roleId != null)
            {
                return (int)roleId;
            }
            throw new Exception($"No role ID found for user ID {userId}");
        }
    }
}
