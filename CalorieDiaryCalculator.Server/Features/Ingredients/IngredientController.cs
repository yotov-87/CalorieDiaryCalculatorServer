using CalorieDiaryCalculator.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<IngredientListingResponseModel>> Mine()
        {
            var userId = User.GetId();

            var result = await this.ingredientsService.ByUser(userId);

            return result;
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
