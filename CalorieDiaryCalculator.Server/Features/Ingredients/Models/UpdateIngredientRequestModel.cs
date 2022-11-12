namespace CalorieDiaryCalculator.Server.Features.Ingredients.Models {
    public class UpdateIngredientRequestModel : CreateIngredientRequestModel {
        public Guid Id { get; set; }
    }
}
