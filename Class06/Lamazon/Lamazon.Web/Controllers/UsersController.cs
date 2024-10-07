using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserCredentialsViewModel userCredentialsViewModel, string returnUrl)
        {
            var userValidationResult = _userService.ValidateUser(userCredentialsViewModel);

            if (userValidationResult is null)
            {
                ModelState.AddModelError("UserLoginError", "User not found!");
                return View(userCredentialsViewModel);
            }
            if (string.IsNullOrEmpty(userValidationResult.Email)) 
            {
                ModelState.AddModelError("UserLoginError", "Invalid email or password");
                return View(userCredentialsViewModel);
            }

            await AuthHelper.SignInUser(userValidationResult, HttpContext);

            if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("/");
            }

            return Redirect(returnUrl);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            var registerResultUserViewModel = _userService.RegisterUser(registerUserViewModel);
            if (registerResultUserViewModel != null)
            {
                await AuthHelper.SignInUser(registerResultUserViewModel, HttpContext);
                return Redirect("/");
            }

            return View(registerUserViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await AuthHelper.SignoutUser(HttpContext);
            return Redirect("/");
        }
    }
}
