namespace CalorieDiaryCalculator.Server.Features.Ingredients {
    public interface IIngredientsService {
        public Task<Guid> Create(string name, uint caloriesPerGram, string imageUrl, bool isPrivate, string userId);
        public Task<IEnumerable<IngredientListingResponseModel>> ByUser(string userId);
    }
}
