using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;
using Newtonsoft.Json;
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
        public int ID { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Permission> Permissions { get; private set; } = new();

        [JsonIgnore]
        public List<UserAccount> Users { get; private set; } = new();
    }
}
