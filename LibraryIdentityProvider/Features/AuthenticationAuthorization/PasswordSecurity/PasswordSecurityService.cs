using Konscious.Security.Cryptography;
using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.ResultAndError;
using System.Security.Cryptography;
using System.Text;

namespace LibraryIdentityProvider.Features.AuthenticationAuthorization.PasswordSecurity
{
    public class PasswordSecurityService
    {
        private static int _keySize = 64;

        IPasswordRepository _passwordRepository;

        public PasswordSecurityService(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task<Result> CreateAndStorePasswordHash(Guid userID, string password)
        {
            byte[] hash = HashPassword(password, out byte[] salt);

            Password pass = new(userID, hash, salt);

            return await _passwordRepository.StorePassword(pass);
        }

        public async Task<Result<bool>> VerifyPassword(Guid userID, string password)
        {
            Result<Password> storedPassResult = await _passwordRepository.RetrieveHashPass(userID);

            if (storedPassResult.IsFailure)
            {
                return Result.Failure(false, storedPassResult.Error!);
            }

            byte[] givenPassHashed = HashPasswordWithSalt(password, storedPassResult.Value.Salt);

            return storedPassResult.Value.PasswordHash.SequenceEqual(givenPassHashed);
        }


        private static byte[] CreateSalt()
        {
            return RandomNumberGenerator.GetBytes(64);
        }

        private static byte[] HashPassword(string password, out byte[] salt)
        {
            salt = CreateSalt();

            return HashPasswordWithSalt(password, salt);
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            Argon2id argon2 = new(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = Environment.ProcessorCount * 2;
            argon2.Iterations = 4;
            argon2.MemorySize = 256;

            return argon2.GetBytes(_keySize);
        }
    }
}
