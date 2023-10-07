using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public interface IRoleRepository
    {
        Task<Result<Role>> GetRole(string roleName);
    }
}