using LibraryIdentityProvider.Entities;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class RoleUser
    {
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public Guid UserID { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
