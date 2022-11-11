using System.ComponentModel.DataAnnotations;
using static CalorieDiaryCalculator.Server.Data.Validation.Ingredient;

namespace CalorieDiaryCalculator.Server.Data.Models {
    public class Ingredient {

        public Guid Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public uint CaloriesPerGram { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsPrivate { get; set; }

        public CalorieDiaryCalculatorUser User { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
