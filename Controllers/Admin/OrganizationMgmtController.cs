using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers.Admin
{
    [Authorize(Roles = "s_admin, admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationMgmtController : ControllerBase
    {
        private readonly IOrganizationMgmtService _organizationMgmtService;
        private APIResponse _apiResponse;
        public OrganizationMgmtController(IOrganizationMgmtService organizationMgmtService)
        {
            _organizationMgmtService = organizationMgmtService;
            _apiResponse = new();
        }
        [HttpGet]
        [Route("organization-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> organizationListAsync()
        {
            try
            {
                var orgList = await _organizationMgmtService.GetOrganizationListAsync();
                if (orgList == null)
                {
                    _apiResponse.Message.Add("User not found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                _apiResponse.Data = orgList;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return _apiResponse;
            }
            catch (Exception ex) {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }
        [HttpPost]
        [Route("create-organization")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateOrganizationPost(OrganizationDTO dto)
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
                var orgName = await _organizationMgmtService.GetOrganizationByConditionAsync(dto.OrgName);
                if (!string.IsNullOrEmpty(orgName))
                {
                    _apiResponse.Message.Add($"Organization Name {dto.OrgName} alrady exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }                
                long createOrganization = await _organizationMgmtService.CreateOrganizationAsync(dto);
                if (createOrganization != 0)
                {
                    _apiResponse.Message.Add("Organization added successfully!");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode= HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Something Went Wrong!");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
            catch (Exception ex) {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
        }
    }
}
