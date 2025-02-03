namespace DemoJWT.AuthModels
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }  // masalan: "products.view"
        public string DisplayName { get; set; }  // masalan: "View Products"
        public string Description { get; set; }
        public string GroupName { get; set; }  // masalan: "Products"

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
