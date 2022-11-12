﻿using CalorieDiaryCalculator.Server.Features.Ingredients.Models;
using CalorieDiaryCalculator.Server.Infrastructure;
using CalorieDiaryCalculator.Server.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CalorieDiaryCalculator.Server.Infrastructure.WebConstants;

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
        [Route(ingredientId)]
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update(UpdateIngredientRequestModel model) {
            var userId = User.GetId();

            var updated = await this.ingredientsService.Update(model.Id, userId, model.Name, model.ImageUrl, model.CaloriesPerGram, model.IsPrivate);

            if (updated == false) {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(ingredientId)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid ingredientId) {
            var userId = User.GetId();

            var result = await this.ingredientsService.Delete(ingredientId, userId);

            if (result == false) {
                return BadRequest();
            }

            return Ok();
        }
    }
}
