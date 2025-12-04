using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using recipe_tracker.Models;

namespace recipe_tracker.Database.Models;

public class Recipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecipeId { get; init; }

    public required RecipeDetails RecipeDetails { get; init; }
    public required List<Instruction> Instructions { get; init; } = [];
    public required List<Ingredient> Ingredients { get; init; } = [];

    [MaxLength(100)] public required string UserName { get; init; }

    public RecipeViewModel ToRecipeViewModel()
    {
        return new RecipeViewModel
        {
            RecipeId = RecipeId,
            RecipeDetails = RecipeDetails,
            Instructions =
                [.. Instructions.Select(i => new InstructionViewModel { Text = i.Text, Id = i.InstructionId })],
            Ingredients =
            [
                .. Ingredients.Select(i => new IngredientViewModel
                    { Detail = i.Detail, Quantity = i.Quantity, Unit = i.Unit, Id = i.IngredientId })
            ],
            UserName = UserName
        };
    }
}

public class RecipeDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [UsedImplicitly]
    public int RecipeDetailsId { get; set; }

    [MaxLength(50)]
    [DisplayName("Recipe Name: ")]
    public required string Title { get; set; }

    [MaxLength(1000)]
    [DisplayName("Description: ")]
    public required string Description { get; set; }

    public RecipeImage? Image { get; set; }

    public static implicit operator RecipeDetails(RecipeDetailsViewModel v)
    {
        using var memoryStream = new MemoryStream();
        v.Image?.CopyToAsync(memoryStream);
        return new RecipeDetails
        {
            Title = v.Title,
            Description = v.Description,
            Image = new RecipeImage
            {
                ImageBytes = memoryStream.ToArray()
            }
        };
    }
}

public class Instruction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? InstructionId { get; init; }

    [MaxLength(500)] public required string Text { get; init; }
}

public class Ingredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IngredientId { get; init; }

    [MaxLength(50)] public required string Detail { get; init; }

    public required int Quantity { get; init; }

    [MaxLength(50)] public required string Unit { get; init; }
}

public class RecipeImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImageId { get; init; }

    public required byte[] ImageBytes { get; set; }
}