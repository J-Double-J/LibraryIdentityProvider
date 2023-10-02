namespace LibraryIdentityProvider.Features.UserManagement
{
    public class UserAccount
    {
        public string Username { get; private set; }
        public string[] Claims { get; private set; } 
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public UserAccount(string username, string[] claims, string email, string firstName, string lastName)
        {
            Username = username;
            Claims = claims;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
