using System.ComponentModel;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Models;

public class RecipeViewModel
{
    public int RecipeId { get; init; }
    public RecipeDetailsViewModel RecipeDetails { get; init; } = new();
    public required List<InstructionViewModel>? Instructions { get; init; } = [];
    public required List<IngredientViewModel>? Ingredients { get; init; } = [];
    public required string UserName { get; set; }

    public Recipe ToRecipeDbModel()
    {
        return new Recipe
        {
            RecipeDetails = RecipeDetails,
            Instructions =
            [
                .. Instructions?.Select(i => new Instruction
                    { Text = i.Text, InstructionId = i.Id }) ?? []
            ],
            Ingredients =
            [
                .. Ingredients?.Select(i => new Ingredient
                {
                    Detail = i.Detail, Quantity = i.Quantity, Unit = i.Unit, IngredientId = i.Id
                }) ?? []
            ],
            UserName = UserName,
            RecipeId = RecipeId
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
            ImageBytes = v.Image?.ImageBytes
        };
    }
}

public class InstructionViewModel
{
    public int? Id { get; init; }
    public required string Text { get; init; }

    public static implicit operator InstructionViewModel(Instruction v)
    {
        return new InstructionViewModel
        {
            Id = v.InstructionId,
            Text = v.Text
        };
    }
}

public class IngredientViewModel
{
    public int? Id { get; init; }
    public required string Detail { get; init; }
    public required int Quantity { get; init; }
    public required string Unit { get; init; }

    public static implicit operator IngredientViewModel(Ingredient v)
    {
        return new IngredientViewModel
        {
            Id = v.IngredientId,
            Detail = v.Detail,
            Quantity = v.Quantity,
            Unit = v.Unit
        };
    }
}