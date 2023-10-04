using LibraryIdentityProvider.Entities;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class Permission : Entity
    {
        public Permission(Guid guid, string name, string description) : base(guid)
        {
            Name = name;

            Description = description;
        }

        /// <summary>
        /// Constructor for EFCore.
        /// </summary>
        private Permission()
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
