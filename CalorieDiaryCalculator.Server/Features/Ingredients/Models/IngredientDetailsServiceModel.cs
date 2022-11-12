namespace CalorieDiaryCalculator.Server.Features.Ingredients.Models
{
    public class IngredientDetailsServiceModel : IngredientListingServiceModel {
        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
