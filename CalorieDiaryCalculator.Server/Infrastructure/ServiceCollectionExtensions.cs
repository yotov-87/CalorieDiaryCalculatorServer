using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CalorieDiaryCalculator.Server.Infrastructure {
    public static class ServiceCollectionExtensions {

        public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration) {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettingsSection");
            services.
                Configure<AppSettings>(applicationSettingsConfiguration);
            var appSettings = applicationSettingsConfiguration.Get<AppSettings>();

            return appSettings;
        }
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
        

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings) {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services
                .AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false // TODO: set to "true" may be
                    };
                });

            return services;
        }
    }
}
