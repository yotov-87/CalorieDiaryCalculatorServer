using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Data.Models.Base;
using CalorieDiaryCalculator.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalorieDiaryCalculator.Server.Data {
    public class CalorieDiaryCalculatorDbContext : IdentityDbContext<CalorieDiaryCalculatorUser> {
        private readonly ICurrentUserService currentUser;
        public CalorieDiaryCalculatorDbContext(DbContextOptions<CalorieDiaryCalculatorDbContext> options, ICurrentUserService currentUser)
            : base(options) {
            this.currentUser = currentUser;
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges() {
            this.ApplyAuditInformation();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Ingredient>()
                .HasOne(ingredient => ingredient.User)
                .WithMany(user => user.Ingredients)
                .HasForeignKey(ingredient => ingredient.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInformation() {

            var a = this.ChangeTracker
                .Entries()
                .ToList();
             a
                .ForEach(entry => {
                    var userName = this.currentUser.GetUserName();

                    if (entry.Entity is IDeletableEntity deletableEntity) {
                        deletableEntity.DeletedOn = DateTime.UtcNow;
                        deletableEntity.DeletedBy = userName;
                        deletableEntity.IsDeleted = true;

                        entry.State = EntityState.Modified;
                    }
                    
                    if (entry.Entity is IEntity entity) {
                        if (entry.State == EntityState.Added) {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = userName;
                        }
                        else if (entry.State == EntityState.Modified) {
                            entity.ModifiedOn = DateTime.UtcNow;
                            entity.ModifiedBy = userName;
                        }
                    }
                });
        }
    }
}