using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Infrastructure;
using CalorieDiaryCalculator.Server.Models.Ingredients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalorieDiaryCalculator.Server.Features {
    public class IngredientController : ApiController {
        private readonly CalorieDiaryCalculatorDbContext data;

        public IngredientController(CalorieDiaryCalculatorDbContext data) {
            this.data = data;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateIngredientRequestModel model) {
            var userId = this.User.GetId();

            var ingredient = new Ingredient {
                Name = model.Name,
                CaloriesPerGram = model.CaloriesPerGram,
                ImageUrl = model.ImageUrl,
                IsPrivate = model.IsPrivate,
                UserId = userId
            };

            this.data.Add(ingredient);

            await this.data.SaveChangesAsync();

            return Created(nameof(this.Create), ingredient.Id);
        }
    }
}
