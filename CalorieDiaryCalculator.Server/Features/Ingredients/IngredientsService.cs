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
                .OrderByDescending(ingredient => ingredient.CreatedOn)
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

        public async Task<bool> Update(Guid id, string userId, string name, string ImageUrl, uint caloriesPerGram, bool isPrivate) {
            var ingredient = await GetIgredientByIdAndUserId(id, userId);

            if (ingredient == null) {
                return false;
            }

            ingredient.Name = name;
            ingredient.CaloriesPerGram = caloriesPerGram;
            ingredient.ImageUrl = ImageUrl;
            ingredient.IsPrivate = isPrivate;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid id, string userId) {
            var ingredient = await GetIgredientByIdAndUserId(id, userId);            

            if (ingredient == null) {
                return false;
            }

            this.data.Ingredients.Remove(ingredient);
            await this.data.SaveChangesAsync();

            return true;
        }

        private async Task<Ingredient> GetIgredientByIdAndUserId(Guid ingredientId, string userId) {
            var ingredient = await this.data
                .Ingredients
                .Where(ingredient => ingredient.Id == ingredientId && ingredient.UserId == userId)
                .FirstOrDefaultAsync();

            return ingredient;
        }
    }
}
