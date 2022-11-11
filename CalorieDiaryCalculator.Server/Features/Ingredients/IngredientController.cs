using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalorieDiaryCalculator.Server.Features.Ingredients
{
    public class IngredientController : ApiController
    {
        private readonly IIngredientsService ingredientsService;

        public IngredientController(IIngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreateIngredientRequestModel model)
        {
            var userId = User.GetId();

            var ingredientId = await this.ingredientsService.Create(model.Name, model.CaloriesPerGram, model.ImageUrl, model.IsPrivate, userId);

            return Created(nameof(this.Create), ingredientId);
        }
    }
}
