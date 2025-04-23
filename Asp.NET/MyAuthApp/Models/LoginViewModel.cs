using System.ComponentModel.DataAnnotations;

namespace MyAuthApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(100, ErrorMessage = "Character limit: 100 Characters!")]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, ErrorMessage = "Password cannot be longer than 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
