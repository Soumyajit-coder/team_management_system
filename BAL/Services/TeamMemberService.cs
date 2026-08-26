using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IMapper _mapper;
        public TeamMemberService(ITeamMemberRepository teamMemberRepository, IMapper mapper)
        {
            _teamMemberRepository = teamMemberRepository;
            _mapper = mapper;
        }
        public async Task<List<TeamMemberDetailsDTO>> GetTeamMembersListAsync()
        {
            var memberList = await _teamMemberRepository.GetTeamMemberListAsync();
            return memberList;
        }
        public async Task<TeamMemberDetailsDTO> GetMemberByUserIdAsync(int userId)
        {
            var memberAssignedTeam = await _teamMemberRepository.GetMemberByUserIdAsync(userId);
            return memberAssignedTeam;
        }
        public async Task<List<TeamMemberDetailsDTO>> GetTeamMembersDetailsAsync(string teamName)
        {
            var teamMemberDetails = await _teamMemberRepository.GetTeamMembersByTeamNameAsync(teamName);
            return teamMemberDetails;
        }
        public async Task<long> CreateTeamMembersAsync(TeamMemberDTO dto)
        {
            TeamMember createTeamMember = _mapper.Map<TeamMember>(dto);
            await _teamMemberRepository.CreateAsync(createTeamMember);
            return createTeamMember.Id;
        }
        public async Task<bool> ExistInTeamMemberAsync(int userId, int TeamId)
        {
            bool ExistInTeamMember = await _teamMemberRepository.GetTeamDetailsByIdAsync(userId, TeamId);
            return ExistInTeamMember;
        }
    }
}
