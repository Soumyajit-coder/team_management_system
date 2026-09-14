using team_management_system.DTO;

namespace team_management_system.BAL.Interfaces
{
    public interface IUserDetailsService
    {
        Task<List<UserDetailsDTO>> GetAllUsersAsync();
        Task<UserDetailsDTO>GetUserByConditionAsync(int id);
        Task<UserDetailsDTO> GetUserByConditionAsync(string condition);
        Task<long> CreateUserAsync(UserDTO dto);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> UpdateUserAsync(long id, UserUpdateDTO dto);
    }
}
