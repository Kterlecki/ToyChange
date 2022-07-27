using System.ComponentModel.DataAnnotations;

namespace ToyChange.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is Needed")]
        public string UserName { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
