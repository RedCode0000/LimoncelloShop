using System.ComponentModel.DataAnnotations;

namespace LimoncelloShop.Api.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Zipcode { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
