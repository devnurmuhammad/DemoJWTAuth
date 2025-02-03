using Microsoft.AspNetCore.Authorization;

namespace DemoJWT.AuthServices
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(permission)
        {
        }
    }
}
