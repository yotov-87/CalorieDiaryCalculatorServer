using CalorieDiaryCalculator.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CalorieDiaryCalculator.Server.Infrastructure {
    public static class ApplicationBuilderExtensions {
        public static void ApplyMigration(this IApplicationBuilder app) {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<CalorieDiaryCalculatorDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
