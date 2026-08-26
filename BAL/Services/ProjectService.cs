using AutoMapper;
using team_management_system.BAL.Interfaces;
using team_management_system.DAL.Entities;
using team_management_system.DAL.Interfaces;
using team_management_system.DAL.Repositories;
using team_management_system.DTO;

namespace team_management_system.BAL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<List<MProject>> GetProjectListAsync()
        {
            var projectList = await _projectRepository.GetAllAsync();
            return projectList;
        }
        public async Task<long> CreateProjectAsync(ProjectDTO dto)
        {
            MProject createProject = _mapper.Map<MProject>(dto);
            if (string.IsNullOrEmpty(createProject.Slug) && !string.IsNullOrEmpty(createProject.ProjectName))
            {
                createProject.Slug = createProject.ProjectName.Trim().ToLower().Replace(" ", "_");
            }
            await _projectRepository.CreateAsync(createProject);
            return createProject.Id;
        }
        public async Task<MProject> GetProjectDetailsByIAsync(int p_id)
        {
            var getProjectDetails = await _projectRepository.GetDetailsByIdAsync(p_id);
            return getProjectDetails;
        }
        public async Task<List<ProjectDetailsDTO>> GetProjectDetailsByOrgNameAsync(string org_name)
        {
            var projectDetails = await _projectRepository.GetProjectDetailsByOrgName(org_name);
            return projectDetails;
        }
        public async Task<String> GetProjectDetailsByName(string p_name)
        {
            var projectName = await _projectRepository.GetDetailsByNameAsync(p => p.ProjectName == p_name, p => p.ProjectName);
            return projectName;
        }
    }
}
