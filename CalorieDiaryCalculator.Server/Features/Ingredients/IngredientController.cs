using CalorieDiaryCalculator.Server.Features.Ingredients.Models;
using CalorieDiaryCalculator.Server.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieDiaryCalculator.Server.Features.Ingredients
{
    [Authorize]
    public class IngredientController : ApiController
    {
        private readonly IIngredientsService ingredientsService;

        public IngredientController(IIngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<IngredientListingServiceModel>> Mine()
        {
            var userId = User.GetId();

            var result = await this.ingredientsService.ByUser(userId);

            return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{ingredientId}")]
        public async Task<ActionResult<IngredientDetailsServiceModel>> Details(Guid ingredientId) {

            var ingredient = await this.ingredientsService.Details(ingredientId);

            return ingredient;

        }

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
