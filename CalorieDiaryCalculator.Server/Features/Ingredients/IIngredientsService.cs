using CalorieDiaryCalculator.Server.Features.Ingredients.Models;

namespace CalorieDiaryCalculator.Server.Features.Ingredients {
    public interface IIngredientsService {
        Task<Guid> Create(string name, uint caloriesPerGram, string imageUrl, bool isPrivate, string userId);

        Task<IEnumerable<IngredientListingServiceModel>> ByUser(string userId);

        Task<IngredientDetailsServiceModel> Details(Guid ingredientId);

        Task<bool> Update(Guid id, string userId, string name, string ImageUrl, uint caloriesPerGram, bool isPrivate);

        Task<bool> Delete(Guid id, string userId);

    }
}
