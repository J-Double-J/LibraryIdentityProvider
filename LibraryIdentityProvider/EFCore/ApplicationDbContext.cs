using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.EFCore
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        async Task<int> IApplicationDbContext.SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
