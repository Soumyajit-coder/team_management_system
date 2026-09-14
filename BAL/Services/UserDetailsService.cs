using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interface;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserDetailsService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDetailsDTO>> GetAllUsersAsync()
        {
            var User = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDetailsDTO>>(User);
        }

        public async Task<long> CreateUserAsync(UserDTO dto)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHasher(dto.Password, out passwordHash, out passwordSalt);
            User createUserDetails = _mapper.Map<User>(dto);
            createUserDetails.PasswordHash = passwordHash;
            createUserDetails.PasswordSalt = passwordSalt;
            await _userRepository.CreateAsync(createUserDetails);
            return createUserDetails.Id;
        }

        public async Task<UserDetailsDTO> GetUserByConditionAsync(int id)
        {
            var userDetails = await _userRepository.GetDetailsAsync(u => u.Id == id);
            return _mapper.Map<UserDetailsDTO>(userDetails);
        }

        public async Task<UserDetailsDTO> GetUserByConditionAsync(string condition)
        {
            var userDetailsByCondition = await _userRepository.GetDetailsAsync(u => u.UserName == condition || u.MobileNo == condition || u.Email == condition);
            return _mapper.Map<UserDetailsDTO>(userDetailsByCondition);
        }
        public async Task<bool> UpdateUserAsync(long id, UserUpdateDTO dto)
        {
            var existingUser = await _userRepository.GetDetailsAsync(u => u.Id == id);
            if (existingUser == null)
            {
                return false;
            }
            _mapper.Map<UserUpdateDTO>(existingUser);
            await _userRepository.UpdateAsync(existingUser);
            return true;
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) 
        {
            if (passwordHash == null || passwordSalt == null || string.IsNullOrEmpty(password))
            {
                return false;
            }
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) 
                        return false;
                }
            }
            return true;
        }

        private void PasswordHasher(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
