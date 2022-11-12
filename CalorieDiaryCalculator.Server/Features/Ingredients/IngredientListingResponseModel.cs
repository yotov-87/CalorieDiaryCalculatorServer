using CalorieDiaryCalculator.Server.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CalorieDiaryCalculator.Server.Features.Ingredients {
    public class IngredientListingResponseModel {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public uint CaloriesPerGram { get; set; }

        public string ImageUrl { get; set; }
    }
}
