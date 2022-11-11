using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalorieDiaryCalculator.Server.Infrastructure {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<CalorieDiaryCalculatorDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services) {
            services
                .AddIdentity<CalorieDiaryCalculatorUser, IdentityRole>(options => {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<CalorieDiaryCalculatorDbContext>();

            return services;
        }
    }
}
