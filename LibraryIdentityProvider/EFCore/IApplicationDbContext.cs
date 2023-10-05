using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;
using LibraryIdentityProvider.Features.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.EFCore
{
    public interface IApplicationDbContext
    {
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Password> Password { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }

        public Task<int> SaveChangesAsync();
    }
}
