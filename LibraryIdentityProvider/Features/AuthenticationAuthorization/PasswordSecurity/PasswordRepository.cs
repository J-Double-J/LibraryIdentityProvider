using LibraryIdentityProvider.EFCore;
using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.ResultAndError;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.Features.AuthenticationAuthorization.PasswordSecurity
{
    public sealed class PasswordRepository : IPasswordRepository
    {
        IApplicationDbContext _context;

        public PasswordRepository(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result> StorePassword(Password password)
        {
            try
            {
                await _context.Password.AddAsync(password);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(new ErrorCode("DatabaseError", "PasswordStorage", ex.GetType().ToString()),
                                      $"Failed to store password: {ex.Message}"));
            }
        }

        public async Task<Result<Password>> RetrieveHashPass(Guid userID)
        {
            try
            {
                Password? password = await _context.Password.Where(p => p.UserID == userID).FirstOrDefaultAsync();

                if (password is null)
                {
                    return Result.Failure<Password>(null,
                                                    new Error(new ErrorCode("DatabaseError", "PasswordRetrieval", "UserNotFound"),
                                                    $"User with ID `{userID}` was not found to have a password stored."));
                }

                return Result.Success(password);
            }
            catch (Exception ex)
            {
                return Result.Failure<Password>(null, new Error(new ErrorCode("DatabaseError", "PasswordRetrieval", ex.GetType().ToString()),
                                      $"Failed to store password: {ex.Message}"));
            }
        }
    }
}
