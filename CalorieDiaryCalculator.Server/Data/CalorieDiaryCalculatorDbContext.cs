using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalorieDiaryCalculator.Server.Data {
    public class CalorieDiaryCalculatorDbContext : IdentityDbContext {
        public CalorieDiaryCalculatorDbContext(DbContextOptions<CalorieDiaryCalculatorDbContext> options)
            : base(options) {
        }
    }
}