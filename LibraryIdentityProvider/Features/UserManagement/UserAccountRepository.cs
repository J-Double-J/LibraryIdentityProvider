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

        public async Task<Result<UserAccount>> CreateUserAccount(CreateUserAccountCommand command)
        {
            UserAccount account = new(Guid.NewGuid(), command.Username, command.Claims, command.Email, command.FirstName, command.LastName);
            _context.UserAccount.Add(account);

            await _context.SaveChangesAsync();

            return Result.Success(account);
        }
    }
}
