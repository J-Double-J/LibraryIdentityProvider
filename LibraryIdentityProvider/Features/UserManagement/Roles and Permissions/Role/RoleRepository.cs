using LibraryIdentityProvider.EFCore;
using LibraryIdentityProvider.Patterns.ResultAndError;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class RoleRepository : IRoleRepository
    {
        private static readonly ErrorCode ROLE_NAME_NOT_FOUND = new("Database", "RoleRepository", "RoleNameNotFound");
        private static readonly ErrorCode ROLE_DB_ERROR = new("Database", "RoleRepository", "DbError");

        private readonly IApplicationDbContext _context;
        public RoleRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Role>> GetRole(string roleName)
        {
            try
            {
                Role? role = await _context.Role.FirstOrDefaultAsync(r => r.Name == roleName);

                if (role is null)
                {
                    return Result.Failure<Role>(null, new Error(ROLE_NAME_NOT_FOUND, $"No role with role name '{roleName}' was found."));
                }

                return Result.Success(role);
            }
            catch (Exception ex)
            {
                return Result.Failure<Role>(null, new Error(ROLE_DB_ERROR, ex.Message));
            }

        }
    }
}
