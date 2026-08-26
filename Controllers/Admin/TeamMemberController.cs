using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.BAL.Services;
using team_management_system.DAL.Entities;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers.Admin
{
    [Authorize(Roles = "s_admin, admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;
        private APIResponse _apiResponse;
        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
            _apiResponse = new();
        }
        [HttpPost]
        [Route("create-team-members")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTeamMemberDetailsAsync([FromBody] TeamMemberDTO dto)
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
                var isExistInTeam = await _teamMemberService.ExistInTeamMemberAsync(dto.UserId, dto.TeamId);
                if (isExistInTeam)
                {
                    _apiResponse.Message.Add($"User is alrady exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                long createTeamMemberDetails = await _teamMemberService.CreateTeamMembersAsync(dto);
                if (createTeamMemberDetails != 0)
                {
                    _apiResponse.Message.Add("Team Member added successfully!");
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
        [Route("team-members-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTeamMembersList()
        {
            try
            {
                var teamMembersList = await _teamMemberService.GetTeamMembersListAsync();
                if (teamMembersList == null)
                {
                    _apiResponse.Message.Add("List Not Found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = teamMembersList;
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
        [Route("get-member-from-team")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetMemberFromTeamAsync(int userId)
        {
            try
            {
                var memberDetails = await _teamMemberService.GetMemberByUserIdAsync(userId);
                if (memberDetails == null)
                {
                    _apiResponse.Message.Add($"No details found for User ID: {userId}");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode= HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = memberDetails;
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
        [Route("get-team-member-details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetTeamMemberDetailsAsync(string teamName)
        {
            try
            {
                var teamDetails = await _teamMemberService.GetTeamMembersDetailsAsync(teamName);
                if (teamDetails == null || !teamDetails.Any())
                {
                    _apiResponse.Message.Add($"No details found for {teamName}");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = teamDetails;
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
