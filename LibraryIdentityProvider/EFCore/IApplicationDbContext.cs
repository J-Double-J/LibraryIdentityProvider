using LibraryIdentityProvider.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.EFCore
{
    public interface IApplicationDbContext
    {
        DbSet<UserAccount> UserAccount { get; set; }
        Task<int> SaveChangesAsync();
    }
}
