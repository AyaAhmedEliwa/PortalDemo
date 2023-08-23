using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Core.Model;
using Portal.Infrastructure.Extend;
using System.Diagnostics;

namespace Portal.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;  //dependency injection
        private readonly IMapper mapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        
        public AccountController(UserManager<ApplicationUser> userManager , IMapper mapper , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

        #region Registration

        public IActionResult Registration()     //[Get] which draw the form in view
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {

            //var user = new ApplicationUser()
            //{
            //    UserName = model.UserName,
            //    Email = model.Email,
            //    IsAgree = model.IsAgree
            //};

            var user = mapper.Map<ApplicationUser>(model);

            if (user.IsAgree == true)
            {
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return Redirect("Login");
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "you should agree about the roles");
            }

            return View(model);
        }

        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["error"] = "User Not Found";
            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Account Not Found");
                }
            }
            return View(model);
        }

        #endregion

        #region Sign Out

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPassword model )
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {                
                // Generate Token
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                EventLog log = new EventLog();
                log.Source = "Hr System";
                log.WriteEntry(passwordResetLink, EventLogEntryType.Information);
            }   
            return View("ConfirmationForgetPassword");
        }

        public IActionResult ConfirmationForgetPassword()
        {
            return View();
        }
        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string Email, string Token)
        {

            if (Email == null || Token == null)
                TempData["error"] = "account not registered";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmationResetPassword");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }
        public IActionResult ConfirmationResetPassword()
        {
            return View();
        }
        #endregion
    }
}
