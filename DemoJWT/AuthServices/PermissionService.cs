using DemoJWT.AuthModels;
using JWTAuthentication.NET6._0.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoJWT.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PermissionService(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AssignPermissionToRoleAsync(string roleId, int permissionId)
        {
            var exists = await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (!exists)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignPermissionToUserAsync(string userId, int permissionId)
        {
            var exists = await _context.UserPermissions
                .AnyAsync(up => up.UserId == userId && up.PermissionId == permissionId);

            if (!exists)
            {
                _context.UserPermissions.Add(new UserPermission
                {
                    UserId = userId,
                    PermissionId = permissionId
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<bool> UserHasPermissionAsync(string userId, string permission)
        {
            var hasPermission = await _context.UserPermissions
                .Include(up => up.Permission)
                .AnyAsync(up => up.UserId == userId && up.Permission.Name == permission);

            if (hasPermission) return true;

            var roles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            return await _context.RolePermissions
                .Include(rp => rp.Permission)
                .AnyAsync(rp => roles.Contains(rp.RoleId) && rp.Permission.Name == permission);
        }
    }
}
