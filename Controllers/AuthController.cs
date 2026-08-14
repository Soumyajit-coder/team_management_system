using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using team_management_system.BAL.Interfaces;
using team_management_system.BAL.Services;
using team_management_system.DAL;
using team_management_system.DTO;
using team_management_system.Helper;

namespace team_management_system.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService;
        private readonly IConfiguration _config;
        private readonly IRoleService _roleService;
        private readonly IUserHasRoleService _userHasRoleService;
        private readonly IRoleHasPermissionService _roleHasPermissionService;
        private teamManagementSystemDBContext _dbContext;
        private readonly APIResponse _apiResponse;

        public AuthController(IUserDetailsService userDetailsService, IConfiguration config, teamManagementSystemDBContext dbContext, IUserHasRoleService userHasRoleService, IRoleHasPermissionService roleHasPermissionService)
        {
            _userDetailsService = userDetailsService;
            _userHasRoleService = userHasRoleService;
            _roleHasPermissionService = roleHasPermissionService;
            _config = config;
            _dbContext = dbContext;
            _apiResponse = new();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login(AuthDTO dto)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserByConditionAsync(dto.UserName);
                if (userDetails == null) {
                    _apiResponse.Message.Add("User name or password doesn't exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var role_name = await _userHasRoleService.GetAssignRoleName(userDetails.Id);
                if (role_name == null)
                {
                    _apiResponse.Message.Add("Role name is not exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var roleId = await _userHasRoleService.GetAssignedRoleId(userDetails.Id);
                if (roleId == null)
                {
                    _apiResponse.Message.Add("Role ID is not exist");
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var permissionName = await _roleHasPermissionService.GetAssignPermissionNameAsync(roleId);
                bool isPasswordMatch = _userDetailsService.VerifyPasswordHash(dto.Password, userDetails.PasswordHash, userDetails.PasswordSalt);
                if (isPasswordMatch)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDetails.UserName.ToString()),
                        new Claim(ClaimTypes.Role, role_name.ToString())
                    };
                    if (permissionName != null)
                    {
                        foreach (var slug in permissionName)
                        {
                            claims.Add(new Claim("Permission", slug));
                        }
                    }
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Auth:TokenKey").Value));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: _config.GetSection("Auth:Issuer").Value,
                        audience: _config.GetSection("Auth:Audience").Value,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(3),
                        signingCredentials: signingCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    _apiResponse.Message.Add("Login Successful");
                    _apiResponse.Data = tokenString;
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }
                _apiResponse.Message.Add("Login Failed");
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return _apiResponse;
            }
            catch (Exception ex) {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _apiResponse.Message.Add(detailedError);
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return _apiResponse;
            }
        }
    }
}
