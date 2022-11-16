using CalorieDiaryCalculator.Server.Data.Models.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CalorieDiaryCalculator.Server.Data.Models {
    public class CalorieDiaryCalculatorUser : IdentityUser, IEntity {
        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    }
}
