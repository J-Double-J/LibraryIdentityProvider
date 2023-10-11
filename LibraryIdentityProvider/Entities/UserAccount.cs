
using LibraryIdentityProvider.Features.UserManagement;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;

namespace LibraryIdentityProvider.Entities
{
    public class UserAccount : Entity
    {
        public static readonly int USERNAME_MIN_LENGTH = 3;
        public static readonly int PASSWORD_MIN_LENGTH = 6;
        public static readonly int FIRSTNAME_MAX_LENGTH = 64;
        public static readonly int LASTNAME_MAX_LENGTH = 64;

        /// <summary>
        /// Constructor for EFCore
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private UserAccount() : base(Guid.Empty)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public UserAccount(Guid guid, string username, List<Role> roles, string email, string firstName, string lastName)
            : base(guid)
        {
            Username = username;
            Roles = roles;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Username { get; private set; }
        public List<Role> Roles { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
