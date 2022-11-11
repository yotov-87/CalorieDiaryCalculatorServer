using Microsoft.AspNetCore.Identity;

namespace CalorieDiaryCalculator.Server.Data.Models {
    public class CalorieDiaryCalculatorUser : IdentityUser {
        public IEnumerable<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

        //public IEnumerable<Plate> Plates { get; set; } = new HashSet<Plate>();
    }
}
