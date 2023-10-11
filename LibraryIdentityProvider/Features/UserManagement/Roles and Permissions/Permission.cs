using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions
{
    public class Permission
    {
        public Permission(string name, string description)
        {
            Name = name;

            Description = description;
        }

        /// <summary>
        /// Constructor for EFCore.
        /// </summary>
        private Permission()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Role> Roles { get; private set; } = new();

        //public List<RolePermission> RolePermissions { get; private set; } = new();
    }
}
