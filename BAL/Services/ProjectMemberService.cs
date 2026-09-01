using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DAL.Repositories;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IMapper _mapper;
        public ProjectMemberService(IProjectMemberRepository projectMemberRepository, IMapper mapper)
        {
            _projectMemberRepository = projectMemberRepository;
            _mapper = mapper;
        }
        public async Task<List<ProjectMemberDetailsDTO>> GetProjectMemeberList()
        {
            var projectMembersList = await _projectMemberRepository.GetProjectDetailsList();
            return projectMembersList;
        }
        public async Task<long> CreateProjectMember(ProjectMemberDTO dto)
        {
            ProjectMember createProjectMember = _mapper.Map<ProjectMember>(dto);
            await _projectMemberRepository.CreateAsync(createProjectMember);
            return createProjectMember.Id;
        }
        public async Task<List<ProjectMemberDetailsDTO>> SearchProjectMemeberListByProjectName(string p_name)
        {
            var projectMembersListByName = await _projectMemberRepository.SerachByProjectName(p_name);
            return projectMembersListByName;
        }
        public async Task<bool> MemberActiveDeactiveToggle(long id)
        {   
            var result = await _projectMemberRepository.MembersActiveDeactiveAsync(id);
            return result;
        }
    }
}
