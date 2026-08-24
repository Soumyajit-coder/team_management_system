using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class OrganizationMemberService : IOrganizationMemberService
    {
        private readonly IOrganizationMembersRepository _organizationMemberRepository;
        private IMapper _mapper;
        public OrganizationMemberService(IOrganizationMembersRepository organizationMemberRepository, IMapper mapper)
        {
            _organizationMemberRepository = organizationMemberRepository;
            _mapper = mapper;
        }
        public async Task<List<OrganizationMembersDetailsDTO>> GetOrganizationMembersListAsync()
        {
            var orgMembersList =  await _organizationMemberRepository.GetOrgMemeberListAsync();
            return orgMembersList;

        }
        public async Task<long> CreateMembersForOrgAsync(OrganizationMembersDTO dto)
        {
            OrganizationMember createMember = _mapper.Map<OrganizationMember>(dto);
            await _organizationMemberRepository.CreateAsync(createMember);
            return createMember.Id;
        }
        public async Task<String> GetOrgUserNameById(int userId)
        {
            var userName = await _organizationMemberRepository.GetNameByIdAsync(userId);
            return userName;
        }
    }
}
