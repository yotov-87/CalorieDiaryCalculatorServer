using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;

namespace CalorieDiaryCalculator.Server.Features.Ingredients {
    public class IngredientsService : IIngredientsService {
        private readonly CalorieDiaryCalculatorDbContext data;

        public IngredientsService(CalorieDiaryCalculatorDbContext data) {
            this.data = data;
        }
        public async Task<Guid> Create(string name, uint caloriesPerGram, string imageUrl, bool isPrivate, string userId) {
            var ingredient = new Ingredient {
                Name = name,
                CaloriesPerGram = caloriesPerGram,
                ImageUrl = imageUrl,
                IsPrivate = isPrivate,
                UserId = userId
            };

            data.Add(ingredient);

            await data.SaveChangesAsync();

            return ingredient.Id;
        }
    }
}
