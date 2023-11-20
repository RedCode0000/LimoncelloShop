using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LimoncelloShop.Domain.Objects
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }

        public string FullName => $"{FirstName} {(MiddleName != null ? MiddleName + " " : "")}{LastName}";

    }
}
