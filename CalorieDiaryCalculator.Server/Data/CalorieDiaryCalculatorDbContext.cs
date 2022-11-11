using CalorieDiaryCalculator.Server.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalorieDiaryCalculator.Server.Data {
    public class CalorieDiaryCalculatorDbContext : IdentityDbContext<CalorieDiaryCalculatorUser> {
        public CalorieDiaryCalculatorDbContext(DbContextOptions<CalorieDiaryCalculatorDbContext> options)
            : base(options) {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        //public DbSet<Plate> Plates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Ingredient>()
                .HasOne(ingredient => ingredient.User)
                .WithMany(user => user.Ingredients)
                .HasForeignKey(ingredient => ingredient.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Plate>()
            //    .HasOne(plate => plate.User)
            //    .WithMany(user => user.Plates)
            //    .HasForeignKey(plate => plate.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}