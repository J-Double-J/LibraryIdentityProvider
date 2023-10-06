namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class RolePermission
    {
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public int PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}
