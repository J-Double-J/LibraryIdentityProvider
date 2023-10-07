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

        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<RoleUser> RoleUser { get; set; }

        async Task<int> IApplicationDbContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>(b => 
                {
                    b.HasKey(u => u.Id);
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

            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleID, rp.PermissionID });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleID);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionID);

            modelBuilder.Entity<RoleUser>().HasKey(ru => new { ru.RoleID, ru.UserID });

            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(ru => ru.RoleID);

            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.UserAccount)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(rp => rp.UserID);
        }
    }
}
