using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Lamazon.Web.Helpers
{
    public static class AuthHelper
    {
        public static async Task SignInUser(UserViewModel userViewModel, HttpContext httpContext)
        {
            // Creating a list of claims to represent the user's identity and roles
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userViewModel.Id.ToString()),
                new Claim(ClaimTypes.Email, userViewModel.Email),
                new Claim(ClaimTypes.Name, userViewModel.FullName),
                new Claim(ClaimTypes.Role, userViewModel.Role.Key),
                new Claim(ClaimTypes.PrimaryGroupSid, userViewModel.Id.ToString()),
            };

            // Creating a ClaimsIdentity with the specified authentication scheme (Cookie Authentication)
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign in the user and create the authentication cookie
            await httpContext.SignInAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                principal: new ClaimsPrincipal(claimsIdentity),
                properties: new AuthenticationProperties
                {
                    IsPersistent = true // Keeps the user logged in across browser sessions
                }
            );
        }

        public static async Task SignoutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
    }
}
