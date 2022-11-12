using CalorieDiaryCalculator.Server.Features.Ingredients.Models;

namespace CalorieDiaryCalculator.Server.Features.Ingredients {
    public interface IIngredientsService {
        public Task<Guid> Create(string name, uint caloriesPerGram, string imageUrl, bool isPrivate, string userId);

        public Task<IEnumerable<IngredientListingServiceModel>> ByUser(string userId);

        public Task<IngredientDetailsServiceModel> Details(Guid ingredientId);

    }
}
