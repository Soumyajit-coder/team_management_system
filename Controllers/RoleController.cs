using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_management_system.BAL.Interfaces;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers
{
    [Authorize(Roles = "s_admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
            _apiResponse = new();
        }
        [HttpGet]
        [Route("role-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllRole()
        {
            try
            {
                var roles = await _roleService.GetAllRoleAsync();
                if (roles == null)
                {
                    _apiResponse.Message.Add("Roles are not found");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Data = roles;
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
        [HasPermission("user.create")]
        [HttpPost]
        [Route("create-role")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRolePostAsync(RoleDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    _apiResponse.Message.Add("Something went wrong!");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var existRole = await _roleService.GetRoleByConditionAsync(dto.RoleName);
                if (existRole != null)
                {
                    _apiResponse.Message.Add("Role name already exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode= HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                //var existSlug = await _roleService.GetRoleByConditionAsync(dto.RoleSlug);
                //if (existSlug != null)
                //{
                //    _apiResponse.Message.Add("Role slug is already exist");
                //    _apiResponse.Status = false;
                //    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                //    return _apiResponse;
                //}
                var createRole = await _roleService.CreateRoleAsync(dto);
                if (createRole != 0)
                {
                    _apiResponse.Message.Add("Role added successfully");
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Something went wrong");
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
    }
}
