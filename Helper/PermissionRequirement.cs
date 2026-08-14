using Microsoft.AspNetCore.Authorization;

namespace team_management_system.Helper
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string permissionName { get; }
        public PermissionRequirement(string permissionName)
        {
            this.permissionName = permissionName;
        }
    }
}
