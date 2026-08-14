using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

namespace team_management_system.Helper
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userPermission = context.User.Claims.Where(c => c.Type == "Permission").Select(c => c.Value);
            if (userPermission.Contains(requirement.permissionName))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
