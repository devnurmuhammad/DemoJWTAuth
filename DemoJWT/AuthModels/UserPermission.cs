using Microsoft.AspNetCore.Identity;

namespace DemoJWT.AuthModels
{
    public class UserPermission
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // FK from AspNetUsers
        public int PermissionId { get; set; } // FK from Permissions

        public IdentityUser User { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}
