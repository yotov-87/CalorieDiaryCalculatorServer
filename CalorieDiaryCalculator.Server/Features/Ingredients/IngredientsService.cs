using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Features.Ingredients.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<IngredientListingServiceModel>> ByUser(string userId) {
            var result = await this.data
                .Ingredients
                .Where(ingradient => ingradient.UserId == userId)
                .Select(ingredient => new IngredientListingServiceModel {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    CaloriesPerGram = ingredient.CaloriesPerGram,
                    ImageUrl = ingredient.ImageUrl
                })
                .ToListAsync();

            return result;
        }

        public async Task<IngredientDetailsServiceModel> Details(Guid ingredientId) {
            var result = await this.data
                .Ingredients
                .Where(ingredient => ingredient.Id == ingredientId)
                .Select(ingredient => new IngredientDetailsServiceModel {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    CaloriesPerGram = ingredient.CaloriesPerGram,
                    ImageUrl = ingredient.ImageUrl,
                    UserId = ingredient.UserId,
                    UserName = ingredient.User.UserName
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
