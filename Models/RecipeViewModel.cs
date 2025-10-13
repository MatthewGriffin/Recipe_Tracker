using System.ComponentModel;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Models;

public class RecipeViewModel
{
    public int RecipeId { get; init; }
    public RecipeDetailsViewModel RecipeDetails { get; init; } = new();
    public required List<InstructionViewModel> Instructions { get; init; }
    public required List<IngredientViewModel> Ingredients { get; init; }
    public UserViewModel User { get; init; } = new();

    public Recipe ToRecipeDbModel()
    {
        return new Recipe
        {
            RecipeDetails = RecipeDetails,
            Instructions =
                [.. Instructions.Select(i => new Instruction { Text = i.Text })],
            Ingredients =
            [
                .. Ingredients.Select(i => new Ingredient
                    { Detail = i.Detail, Quantity = i.Quantity, Unit = i.Unit })
            ],
            User = User
        };
    }
}

public class RecipeDetailsViewModel
{
    [DisplayName("Recipe Name: ")] public string Title { get; init; } = "";

    [DisplayName("Description: ")] public string Description { get; init; } = "";

    [DisplayName("Image: ")] public IFormFile? Image { get; init; }

    public byte[]? ImageBytes { get; private init; }

    public static implicit operator RecipeDetailsViewModel(RecipeDetails v)
    {
        return new RecipeDetailsViewModel
        {
            Title = v.Title,
            Description = v.Description,
            ImageBytes = v.Image?.Image
        };
    }
}

public class InstructionViewModel
{
    public required string Text { get; init; }
}

public class IngredientViewModel
{
    public required string Detail { get; init; }
    public required int Quantity { get; init; }
    public required string Unit { get; init; }
}