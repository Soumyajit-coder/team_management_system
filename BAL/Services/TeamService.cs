using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DAL.Repositories;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private IMapper _mapper;
        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        public async Task<List<TeamDetailsDTO>> GetTeamsListAsync()
        {
            var teams = await _teamRepository.GetTeamListAsync();
            return teams;
        }
        public async Task<long> CreateTeamDetails(TeamDTO dto)
        {
            MTeam createTeam = _mapper.Map<MTeam>(dto);
            await _teamRepository.CreateAsync(createTeam);
            return createTeam.Id;
        }
        public async Task<String> TeamLeadNameAsync(int userId)
        {
            var TLName = await _teamRepository.GetTeamLeadNameAsync(userId);
            return TLName;
        }
    }
}
