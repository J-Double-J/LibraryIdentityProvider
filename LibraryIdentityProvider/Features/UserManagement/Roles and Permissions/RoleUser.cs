using LibraryIdentityProvider.Entities;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class RoleUser
    {
        public int RoleID { get; set; }
        public Guid UserAccountID { get; set; }
    }
}
