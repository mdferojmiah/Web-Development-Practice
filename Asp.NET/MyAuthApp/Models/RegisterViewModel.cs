using System.ComponentModel.DataAnnotations;

namespace MyAuthApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, ErrorMessage = "Password cannot be longer than 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
