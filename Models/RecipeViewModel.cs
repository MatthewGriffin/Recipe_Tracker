using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Models
{
    public class RecipeViewModel
    {
        public RecipeDetailsViewModel RecipeDetails { get; set; } = new RecipeDetailsViewModel();
        public string Instructions { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public ICollection<Instruction> InstructionList { get; set; } = [];
        public ICollection<Ingredient> IngredientList { get; set; } = [];

        public Recipe ToRecipeDBModel()
        {
            return new Recipe()
            {
                RecipeDetails = RecipeDetails,
                Instructions = [.. Instructions.Replace("\r\n", ",").Split(',').Select(s => new Instruction() { Text = s })],
                Ingredients = [.. Ingredients.Replace("\r\n", ",").Split(',').Select(s => new Ingredient() { Detail = s })],
            };
        }
    }

    public class RecipeDetailsViewModel
    {
        [DisplayName("Recipe Name: ")]
        public string Title { get; set; } = "";
        [DisplayName("Description: ")]
        public string Description { get; set; } = "";
        [DisplayName("Servings: ")]
        [Range(1, 999, ErrorMessage = "{0} can only be between {1} and {2}")]
        public int Servings { get; set; } = 0;
        [Range(1, 999, ErrorMessage = "{0} can only be between {1} and {2}")]
        [DisplayName("Prep Time (Mins): ")]
        public int PrepMins { get; set; } = 0;
        [Range(1, 999, ErrorMessage = "{0} can only be between {1} and {2}")]
        [DisplayName("Cook Time (Mins): ")]
        public int CookMins { get; set; } = 0;
        [DisplayName("Tags: ")]
        public string Tags { get; set; } = "";
        [DisplayName("Image: ")]
        public IFormFile? Image { get; set; }
        public byte[]? ImageBytes { get; set; }

        public static implicit operator RecipeDetailsViewModel(RecipeDetails v)
        {
            return new RecipeDetailsViewModel()
            {
                Title = v.Title,
                Description = v.Description,
                Servings = v.Servings,
                PrepMins = v.PrepMins,
                CookMins = v.CookMins,
                Tags = v.Tags,
                ImageBytes = v.Image?.Image,
            };
        }
    }

    public class IngredientsViewModel()
    {
        [DisplayName("Ingredient Name")]
        public string Detail { get; set; } = "";
    }
}