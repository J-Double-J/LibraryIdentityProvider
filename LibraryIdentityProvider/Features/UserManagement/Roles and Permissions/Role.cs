using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public class Role : Entity
    {
        public Role(Guid guid, string name, string description) : base(guid)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor for EFCore.
        /// </summary>
        private Role()
            : base(Guid.Empty)
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; private set; } = new();
    }
}
