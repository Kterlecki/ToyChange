using System.ComponentModel.DataAnnotations;

namespace ToyChange.ViewModel
{
    public class RegisterVM
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Needed")]
        public string EmailAddress { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username must be entered")]
        public string UserName { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name must be entered")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "Password must be a minimum of {2} characters", MinimumLength = 5)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

        [Required(ErrorMessage = "Confirm password is needed")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords entered do not match")]
        public string ConfirmPassword { get; set; }
    }
}
