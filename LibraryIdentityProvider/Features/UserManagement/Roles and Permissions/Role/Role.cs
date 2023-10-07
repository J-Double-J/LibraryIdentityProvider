using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public class Role
    {
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor for EFCore.
        /// </summary>
        private Role()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; private set; } = new();
        public List<RoleUser> RoleUsers { get; private set; } = new();
    }
}
