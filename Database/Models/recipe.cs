using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using recipe_tracker.Models;

namespace recipe_tracker.Database.Models
{
    public class Recipe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeID { get; set; }
        public required RecipeDetails RecipeDetails { get; set; }
        public required ICollection<Instruction> Instructions { get; set; } = [];
        public required ICollection<Ingredient> Ingredients { get; set; } = [];

        public RecipeViewModel ToRecipeViewModel()
        {
            return new RecipeViewModel()
            {
                RecipeDetails = RecipeDetails,
                Instructions = string.Join("\r\n", Instructions.Select(x => x.Text)),
                Ingredients = string.Join("\r\n", Instructions.Select(x => x.Text)),
                InstructionList = Instructions,
                IngredientList = Ingredients,
            };
        }
    }

    public class RecipeDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeDetailsID { get; set; }
        [DisplayName("Recipe Name: ")]
        public required string Title { get; set; }
        [DisplayName("Description: ")]
        public required string Description { get; set; }
        [DisplayName("Servings: ")]
        public required int Servings { get; set; } = 0;
        [DisplayName("Prep Time (Mins): ")]
        public required int PrepMins { get; set; } = 0;
        [DisplayName("Cook Time (Mins): ")]
        public required int CookMins { get; set; } = 0;
        [DisplayName("Tags: ")]
        public required string Tags { get; set; }
        public RecipeImage? Image { get; set; }

        public static implicit operator RecipeDetails(RecipeDetailsViewModel v)
        {
            using (var memoryStream = new MemoryStream())
            {
                v.Image?.CopyToAsync(memoryStream);
                return new RecipeDetails()
                {
                    Title = v.Title,
                    Description = v.Description,
                    Servings = v.Servings,
                    PrepMins = v.PrepMins,
                    CookMins = v.CookMins,
                    Tags = v.Tags,
                    Image = new RecipeImage()
                    {
                        Image = memoryStream.ToArray()
                    },
                };
            }
        }
    }

    public class Instruction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstructionID { get; set; }
        public required string Text { get; set; }
    }

    public class Ingredient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientID { get; set; }
        public required string Detail { get; set; }
    }

    public class RecipeImage()
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        public required byte[] Image { get; set; }
    }
}