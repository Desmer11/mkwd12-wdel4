using Lamazon.Domain.Constants;
using System.Security.Claims;

namespace Lamazon.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static string DisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        }

        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(Roles.Admin);
        }
    }
}
