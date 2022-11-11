using CalorieDiaryCalculator.Server.Data.Models;
using System.ComponentModel.DataAnnotations;
using static CalorieDiaryCalculator.Server.Data.Validation.Ingredient;

namespace CalorieDiaryCalculator.Server.Models.Ingredients {
    public class CreateIngredientRequestModel {

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public uint CaloriesPerGram { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsPrivate { get; set; }
    }
}
