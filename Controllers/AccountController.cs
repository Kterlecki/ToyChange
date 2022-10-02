using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToyChange.Models;
using ToyChange.ViewModel;

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


        [HttpGet]
        public IActionResult Login(string _returnUrl = null)
        {
            LoginVM loginVM = new LoginVM();
            loginVM.ReturnUrl = _returnUrl ?? Url.Content("~/"); // If returnURL is null return the base URL

            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginVM.EmailAddress);
                if (user != null)
                {
                    var passwordVerification = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (passwordVerification)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            return View(loginVM);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Register(string? _returnUrl = null)
        {
            RegisterVM registerVM = new RegisterVM();
            registerVM.ReturnUrl = _returnUrl;
            return View(registerVM);
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM _registerVM, string? _returnUrl = null)
        {
            _registerVM.ReturnUrl = _returnUrl;
            _returnUrl = _returnUrl ?? Url.Content("~/");

            var user = await userManager.FindByEmailAsync(_registerVM.EmailAddress);
            if (user != null)
            {
                ModelState.AddModelError("EmailAddress", "Email address already in use");
                return View(_registerVM);
            }
            if (ModelState.IsValid)
            {
                var newUser = new User { UserName = _registerVM.UserName, Email = _registerVM.EmailAddress };

                _returnUrl = _returnUrl ?? Url.Content("~/");
                var RegisterResult = await userManager.CreateAsync(newUser, _registerVM.Password);
                if (RegisterResult.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return LocalRedirect(_returnUrl); /// check if you can use a different redirect
                }

                ModelState.AddModelError("Password", "Password entered Doesn't meet criteria, User Not created");

            }
            return View(_registerVM);
        }

    }
}
