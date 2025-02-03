using DemoJWT.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoJWT.Handlers
{
    public class AuthorizePermissionAttribute : TypeFilterAttribute
    {
        public AuthorizePermissionAttribute(string permission) : base(typeof(AuthorizePermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }

    public class AuthorizePermissionFilter : IAsyncActionFilter
    {
        private readonly string _permission;
        private readonly IPermissionService _permissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizePermissionFilter(string permission, IPermissionService permissionService, IHttpContextAccessor httpContextAccessor)
        {
            _permission = permission;
            _permissionService = permissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !await _permissionService.UserHasPermissionAsync(userId, _permission))
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
