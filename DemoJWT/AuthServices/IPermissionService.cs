using DemoJWT.AuthModels;

namespace DemoJWT.Services
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAllPermissionsAsync();
        Task AssignPermissionToRoleAsync(string roleId, int permissionId);
        Task AssignPermissionToUserAsync(string userId, int permissionId);
        Task<bool> UserHasPermissionAsync(string userId, string permission);
    }
}
