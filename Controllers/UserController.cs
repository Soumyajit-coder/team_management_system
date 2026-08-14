using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.BAL.Services;
using team_management_system.DAL.Entities;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserDetailsService _userDetailsService;
        private readonly IUserHasRoleService _userHasRoleService;
        private readonly APIResponse _apiResponse;

        public UserController(IMapper mapper, IUserDetailsService userDetailsService, IUserHasRoleService userHasRoleService)
        {
            _mapper = mapper;
            _userDetailsService = userDetailsService;
            _userHasRoleService = userHasRoleService;
            _apiResponse = new();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> createUser(UserDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }
                var existingUser = await _userDetailsService.GetUserByConditionAsync(dto.UserName);
                var existingEmail = await _userDetailsService.GetUserByConditionAsync(dto.Email);
                var existingMobile = await _userDetailsService.GetUserByConditionAsync(dto.MobileNo);
                if (existingUser != null)
                {
                    _apiResponse.Message.Add("User already exist with this username");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                if (existingEmail != null)
                {
                    _apiResponse.Message.Add("User already exist with this email");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                if (existingMobile != null)
                {
                    _apiResponse.Message.Add("User already exist with this mobile number");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;

                }
                long createNewUser = await _userDetailsService.CreateUserAsync(dto);
                if (createNewUser != 0)
                {
                    _apiResponse.Message.Add("User created successfully!!");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Something Went Wrong!!");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            } catch(Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return _apiResponse;
            }
            
            
        }

        [HasPermission("admin.view")]
        [HttpGet]
        [Route("User-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllUser()
        {
            try
            {
                var userList = await _userDetailsService.GetAllUsersAsync();
                if (userList == null)
                {
                    _apiResponse.Message.Add("User not found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = userList;
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

        [HasPermission("user.create")]
        [HttpPost]
        [Route("GetUserRole/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AssignUserRole(int id, AssignRoleDTO dto)
        {
            try
            {
                var getUserDetails = await _userDetailsService.GetUserByConditionAsync(id);
                if (getUserDetails == null)
                {
                    _apiResponse.Message.Add("No user found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                var isRoleAssigned = await _userHasRoleService.AssignRoleAsync(id, dto.RoleId);
                if (isRoleAssigned)
                {
                    _apiResponse.Message.Add($"Role has been assigned for {getUserDetails.UserName}");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add($"Failed to assign role.");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
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

        [HasPermission("user.view")]
        [HttpGet]
        [Route("Get-User-Details/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> getUserDetailsAsync(int id)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserByConditionAsync(id);
                if (userDetails == null)
                {
                    _apiResponse.Message.Add($"No user found with this {id}");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                _apiResponse.Data = userDetails;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return _apiResponse;
            } catch(Exception ex)
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
