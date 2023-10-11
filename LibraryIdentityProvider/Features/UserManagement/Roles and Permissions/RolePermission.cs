using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class RolePermission
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
    }
}
