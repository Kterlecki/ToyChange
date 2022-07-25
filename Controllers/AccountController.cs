using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToyChange.ViewModel;
using ToyChange.Models;

namespace ToyChange.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Register(string? _returnUrl= null)
        {
            RegisterVM registerVM = new RegisterVM();
            registerVM.ReturnUrl = _returnUrl;  
            return View(registerVM);
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM _registerVM, string? _returnUrl=null)
        {
            _registerVM.ReturnUrl = _returnUrl;

            if(ModelState.IsValid)
            {
                var user = new ToyUser { UserName = _registerVM.UserName, Email = _registerVM.EmailAddress};
                //_returnUrl = _returnUrl ?? Url.Action("Index", "Home");
                _returnUrl = _returnUrl ?? Url.Content("~/");
                var RegisterResult = await userManager.CreateAsync(user, _registerVM.Password);
                if (RegisterResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(_returnUrl); /// check if you can use a different redirect
                }

                ModelState.AddModelError("Password", "Password entered Doesn't meet criteria, User Not created");

            }
            return View(_registerVM);
        }

    }
}
