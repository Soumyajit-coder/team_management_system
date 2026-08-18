using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IRoleHasPermissionService _roleHasPermissionService;
        private APIResponse _apiResponse;
        public PermissionController(IPermissionService permissionService, IRoleHasPermissionService roleHasPermissionService)
        {
            _permissionService = permissionService;
            _roleHasPermissionService = roleHasPermissionService;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("permission-list")]
        public async Task<ActionResult<APIResponse>> GetPermissionList()
        {
            try
            {
                var permissionList = await _permissionService.GetAllPermissionAsync();
                if (permissionList == null)
                {
                    _apiResponse.Message.Add("No Data Found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = permissionList;
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
        [HttpPost]
        [Route("create-permission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePermissionPost(PermissionDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    _apiResponse.Message.Add("Something Went Wrong");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var existPermissionName = await _permissionService.GetPermissionByConditionAsync(dto.PermissionName);
                if (existPermissionName != null)
                {
                    _apiResponse.Message.Add($"Already exist with {existPermissionName}");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotAcceptable;
                    return _apiResponse;
                }
                var createPermission = await _permissionService.CreatePermissionAsync(dto);
                if (createPermission != 0)
                {
                    _apiResponse.Message.Add("Permission added successfully");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode=HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Something Went Wrong..");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
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
        [HttpPost]
        [Route("assigned-permission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AssignedPermission(int roleId, int permissionId)
        {
            try
            {
                var existAssignedPermission = await _roleHasPermissionService.GetRolePermissionByIdAsync(roleId, permissionId);
                if (existAssignedPermission != null)
                {
                    _apiResponse.Message.Add("Permission has already exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var assignedPermission = await _roleHasPermissionService.AssignedPermissionByRoleAsync(roleId, permissionId);
                if (assignedPermission)
                {
                    _apiResponse.Message.Add("Permission has assigned successfully");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Failed to assigned.");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
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
