using System.Security.Claims;

namespace CalorieDiaryCalculator.Server.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user) {
            return user.Claims
                    .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?
                    .Value;
        }
            
    }
}
