using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class UsersLabModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }


        [Required]
        [MaxLength(500)]
        public string FullName { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Enter a Ukrainian phone number pls (format: +380XXXXXXXXX)")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long and must contain at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password are not the same.")]
        public string ConfirmPassword { get; set; }

    }
}