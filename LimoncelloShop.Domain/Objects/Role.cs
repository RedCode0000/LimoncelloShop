using Microsoft.AspNetCore.Identity;

namespace LimoncelloShop.Domain.Objects
{
    public class Role : IdentityRole<Guid>
    {
        public string RoleName { get; set; }

        public Role()
        {
            // Default constructor
        }

        public Role(string roleName)
        {
            // Constructor with only mapped properties
            RoleName = roleName;
        }
    }
}
