using System.ComponentModel.DataAnnotations;

namespace ToyChange.ViewModel
{
    public class LoginVM
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Needed")]
        public string EmailAddress { get; set; }
        //[Display(Name = "User Name")]
        //[Required(ErrorMessage = "User Name is Needed")]
        //public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
