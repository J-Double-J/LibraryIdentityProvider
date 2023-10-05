
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

        public UserAccount(Guid guid, string username, string[] claims, string email, string firstName, string lastName)
            : base(guid)
        {
            Username = username;
            Claims = claims;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Username { get; private set; }
        public string[] Claims { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
