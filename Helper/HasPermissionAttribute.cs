using Microsoft.AspNetCore.Authorization;

namespace team_management_system.Helper
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(policy: permission)
        {
        }
    }
}
