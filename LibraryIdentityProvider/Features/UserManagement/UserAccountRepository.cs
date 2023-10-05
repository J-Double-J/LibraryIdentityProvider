using LibraryIdentityProvider.EFCore;
using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public class UserAccountRepository : IUserAccountRepository
    {
        IApplicationDbContext _context;

        public UserAccountRepository(IApplicationDbContext context)
        {
            _context = context;    
        }

        public async Task<Result<UserAccount>> AddUserAccount(UserAccount userAccount)
        {
            _context.UserAccount.Add(userAccount);

            await _context.SaveChangesAsync();

            return Result.Success(userAccount);
        }
    }
}
