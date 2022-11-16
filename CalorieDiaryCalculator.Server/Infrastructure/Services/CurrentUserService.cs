using CalorieDiaryCalculator.Server.Infrastructure.Extensions;
using System.Security.Claims;

namespace CalorieDiaryCalculator.Server.Infrastructure.Services {
    public class CurrentUserService : ICurrentUserService {
        private readonly ClaimsPrincipal user;


        public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
            this.user = httpContextAccessor.HttpContext?.User;
        }

        public string GetId() {
            return this.user
                .GetId();
        }

        public string GetUserName() {
            return this?.user
                ?.Identity
                ?.Name;
        }
    }
}
