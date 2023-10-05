namespace LibraryIdentityProvider.Entities
{
    public class Password
    {
        public Password(Guid userID, byte[] passwordHash, byte[] salt)
        {
            UserID = userID;
            PasswordHash = passwordHash;
            Salt = salt;
        }

        public int PasswordID { get; private set; }
        public Guid UserID { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] Salt { get; private set; }
    }
}
