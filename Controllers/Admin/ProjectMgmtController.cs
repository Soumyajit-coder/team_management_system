using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.BAL.Services;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMgmtController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private APIResponse _apiResponse;
        public ProjectMgmtController(IProjectService projectService)
        {
            _projectService = projectService;
            _apiResponse = new();
        }
        [HttpPost]
        [Route("create-project-details")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateProjectDetails(ProjectDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    _apiResponse.Message.Add("DTO not found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                var isExistInTeam = await _projectService.GetProjectDetailsByName(dto.ProjectName);
                if (!string.IsNullOrEmpty(isExistInTeam))
                {
                    _apiResponse.Message.Add($"Project Name: {dto.ProjectName} is already exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                long createProjectDetails = await _projectService.CreateProjectAsync(dto);
                if (createProjectDetails != 0)
                {
                    _apiResponse.Message.Add("Project details added successfully!");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Something Went Wrong!");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }
        [HttpGet]
        [Route("get-projects-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetProjectListAsync()
        {
            try
            {
                var projectList = await _projectService.GetProjectListAsync();
                if (projectList == null)
                {
                    _apiResponse.Message.Add("Project List Not Found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = projectList;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }
        [HttpGet]
        [Route("get-project-details-by-name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetProjectDetailsByName(string orgName)
        {
            try
            {
                var projectDetails = await _projectService.GetProjectDetailsByOrgNameAsync(orgName);
                if (projectDetails == null)
                {
                    _apiResponse.Message.Add($"No project found with teh Project Name: {orgName}");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = projectDetails;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }
    }
}
