using LibraryIdentityProvider.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.EFCore
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccount { get; set; }

        async Task<int> IApplicationDbContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>().HasKey(ua => ua.Id);
        }

    }
}
