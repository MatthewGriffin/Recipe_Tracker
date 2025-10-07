using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Models;

public class RecipeViewModel
{
    public int RecipeId { get; init; }
    public RecipeDetailsViewModel RecipeDetails { get; init; } = new();
    public required List<InstructionViewModel> Instructions { get; init; }
    public required List<IngredientViewModel> Ingredients { get; init; }

    public Recipe ToRecipeDbModel()
    {
        return new Recipe
        {
            RecipeDetails = RecipeDetails,
            Instructions = [.. Instructions.Select(i => new Instruction { Text = i.Text })],
            Ingredients = [.. Ingredients.Select(i => new Ingredient { Detail = i.Detail })]
        };
    }
}

public class RecipeDetailsViewModel
{
    [DisplayName("Recipe Name: ")]
    public string Title { get; init; } = "";
    [DisplayName("Description: ")]
    public string Description { get; init; } = "";
    [DisplayName("Servings: ")]
    [Range(1, 999, ErrorMessage = "{0} can only be between {1} and {2}")]
    public int Servings { get; init; }
    [Range(1, 999, ErrorMessage = "{0} can only be between {1} and {2}")]
    [DisplayName("Prep Time (Mins): ")]
    public int PrepMins { get; init; } 
    [Range(1, 999, ErrorMessage = "{0} can only be between {1} and {2}")]
    [DisplayName("Cook Time (Mins): ")]
    public int CookMins { get; init; }
    [DisplayName("Tags: ")]
    public string Tags { get; init; } = "";
    [DisplayName("Image: ")]
    public IFormFile? Image { get; init; }
    public byte[]? ImageBytes { get; private init; }

    public static implicit operator RecipeDetailsViewModel(RecipeDetails v)
    {
        return new RecipeDetailsViewModel
        {
            Title = v.Title,
            Description = v.Description,
            Servings = v.Servings,
            PrepMins = v.PrepMins,
            CookMins = v.CookMins,
            Tags = v.Tags,
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
}