using CalorieDiaryCalculator.Server.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CalorieDiaryCalculator.Server.Features.Ingredients.Models
{
    public class IngredientListingServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public uint CaloriesPerGram { get; set; }

        public string ImageUrl { get; set; }
    }
}
