using CalorieDiaryCalculator.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CalorieDiaryCalculator.Server.Infrastructure {
    public static class ApplicationBuilderExtensions {
        public static void ApplyMigration(this IApplicationBuilder app) {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<CalorieDiaryCalculatorDbContext>();
            dbContext.Database.Migrate();
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app) {
            app
                .UseSwagger()
                .UseSwaggerUI(options => {
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "My CalorieDiaryCalculator API v1");
                    options.RoutePrefix = string.Empty;
                });

            return app;
        }
    }
}
