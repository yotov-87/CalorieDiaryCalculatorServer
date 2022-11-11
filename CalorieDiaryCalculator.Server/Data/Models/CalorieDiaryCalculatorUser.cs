using Microsoft.AspNetCore.Identity;

namespace CalorieDiaryCalculator.Server.Data.Models {
    public class CalorieDiaryCalculatorUser : IdentityUser {
        public IEnumerable<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    }
}
