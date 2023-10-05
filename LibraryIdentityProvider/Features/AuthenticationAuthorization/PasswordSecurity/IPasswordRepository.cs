using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.AuthenticationAuthorization.PasswordSecurity
{
    public interface IPasswordRepository
    {
        Task<Result<Password>> RetrieveHashPass(Guid userID);
        Task<Result> StorePassword(Password password);
    }
}