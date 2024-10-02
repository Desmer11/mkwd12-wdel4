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

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var primarySid = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimaryGroupSid)?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(primarySid))
            {
                return 0;
            }
            return int.Parse(primarySid);
        }
    }
}
