using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.UserManagement;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.EFCore
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccount { get; set; }

        public DbSet<Password> Password { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Permission> Permission { get; set; }

        async Task<int> IApplicationDbContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>(b => 
                {
                    b.HasKey(u => u.ID);
                    b.ToTable("UserAccount");
                });

            modelBuilder.Entity<Password>(b => {
                b.HasKey(p => p.PasswordID);
                b.Property(p => p.UserID).IsRequired();

                b.HasOne<UserAccount>()
                    .WithOne()
                    .HasForeignKey<Password>(p => p.UserID);

                b.ToTable("Password");
            });

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>();

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<RoleUser>();
        }
    }
}
