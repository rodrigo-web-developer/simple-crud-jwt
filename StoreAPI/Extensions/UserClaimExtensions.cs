using System.Security.Claims;

namespace StoreAPI.Extensions
{
    public static class UserClaimExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");
        }

        public static long? GetPartnerId(this ClaimsPrincipal user)
        {
            var partnerIdClaim = user.FindFirst("PartnerId");
            if (partnerIdClaim != null && long.TryParse(partnerIdClaim.Value, out long partnerId))
            {
                return partnerId;
            }
            return null; // ou lançar uma exceção, dependendo do seu caso de uso
        }
    }
}
