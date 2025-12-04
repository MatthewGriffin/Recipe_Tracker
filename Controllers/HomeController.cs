using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_tracker.Database;
using recipe_tracker.Database.Models;
using recipe_tracker.Models;

namespace recipe_tracker.Controllers;

public class HomeController(ILogger<HomeController> logger, RecipeTrackerContext dbContext) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var recipes = dbContext.Recipes.Include(r => r.RecipeDetails).Include(r => r.RecipeDetails.Image)
            .Include(r => r.Instructions).Include(r => r.Ingredients)
            .OrderByDescending(r => r.RecipeId)
            .Select(r => r.ToRecipeViewModel()).ToList();
        return View(recipes);
    }

    [HttpGet]
    public IActionResult ViewRecipe(int recipeId)
    {
        logger.LogInformation("Requesting recipe {id}", recipeId);
        var recipe = dbContext.Recipes.Include(r => r.RecipeDetails).Include(r => r.RecipeDetails.Image)
            .Include(r => r.Instructions).Include(r => r.Ingredients)
            .First(recipe => recipe.RecipeId == recipeId);
        var vm = recipe.ToRecipeViewModel();
        logger.LogInformation("Recipe {id} found loading view page", recipeId);
        return View(vm);
    }

    [Authorize]
    [HttpGet]
    public IActionResult AddRecipe()
    {
        return View(new RecipeViewModel { Instructions = [], Ingredients = [], UserName = "" });
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddRecipe(RecipeViewModel recipe)
    {
        if (!ModelState.IsValid && User.Identity?.Name == null)
        {
            logger.LogWarning("AddRecipeViewModel is not valid aborting");
            return View(recipe);
        }

        recipe.UserName = User.Identity?.Name ?? "";
        var dbRecipe = recipe.ToRecipeDbModel();
        logger.LogInformation("{Title} recipe is valid saving to db", dbRecipe.RecipeDetails.Title);
        dbContext.Recipes.Add(dbRecipe);
        dbContext.SaveChanges();
        logger.LogInformation("Redirecting user to view recipe page");
        return RedirectToAction("ViewRecipe", new
        {
            recipeId = dbRecipe.RecipeId
        });
    }

    [HttpGet]
    [Authorize]
    public IActionResult EditRecipe(int recipeId)
    {
        logger.LogInformation("Requesting recipe {id}", recipeId);
        var recipeDb = GetRecipeById(recipeId);
        var vm = recipeDb.ToRecipeViewModel();
        logger.LogInformation("Recipe {id} found loading edit page", recipeId);
        return View(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditRecipe(RecipeViewModel recipe)
    {
        if (!ModelState.IsValid || User.Identity?.Name == null)
        {
            logger.LogWarning("AddRecipeViewModel is not valid aborting");
            return View(recipe);
        }

        var currentRecipe = GetRecipeById(recipe.RecipeId);
        var updatedRecipe = recipe.ToRecipeDbModel();

        currentRecipe.RecipeDetails.Title = updatedRecipe.RecipeDetails.Title;
        currentRecipe.RecipeDetails.Description = updatedRecipe.RecipeDetails.Description;

        if (updatedRecipe.RecipeDetails.Image?.ImageBytes.Length > 0)
        {
            string[] acceptedFileExtensions =
            [
                ".png", ".jpeg", ".jpg"
            ];
            if (!acceptedFileExtensions.Any(ff => recipe.RecipeDetails.Image?.FileName.Contains(ff) ?? false))
            {
                logger.LogWarning("AddRecipeViewModel image is not correct format!");
                ModelState.AddModelError("RecipeDetails.Image", "Image is invalid please upload a valid filetype");
                return View(recipe);
            }

            //if already exists keep image object and just update image value
            if (currentRecipe.RecipeDetails.Image != null && updatedRecipe.RecipeDetails.Image.ImageBytes.Length != 0)
                currentRecipe.RecipeDetails.Image.ImageBytes = updatedRecipe.RecipeDetails.Image.ImageBytes;
            //else create new image object
            else if (updatedRecipe.RecipeDetails.Image.ImageBytes.Length != 0)
                currentRecipe.RecipeDetails.Image = updatedRecipe.RecipeDetails.Image;
        }

        currentRecipe.Instructions.AddRange(updatedRecipe.Instructions.Where(i => i.InstructionId == null));
        currentRecipe.Ingredients.AddRange(updatedRecipe.Ingredients.Where(i => i.IngredientId == null));

        dbContext.RemoveRange(currentRecipe.Instructions.Except(updatedRecipe.Instructions));
        dbContext.RemoveRange(currentRecipe.Ingredients.Except(updatedRecipe.Ingredients));

        dbContext.UpdateRange(updatedRecipe.Instructions);
        dbContext.UpdateRange(updatedRecipe.Ingredients);

        await dbContext.SaveChangesAsync();
        logger.LogInformation("Redirecting user to view recipe page");
        return RedirectToAction("ViewRecipe", new
        {
            recipeId = recipe.RecipeId
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private Recipe GetRecipeById(int recipeId)
    {
        return dbContext.Recipes.Include(r => r.RecipeDetails).Include(r => r.RecipeDetails.Image)
            .Include(r => r.Instructions).Include(r => r.Ingredients)
            .First(recipe => recipe.RecipeId == recipeId);
    }
}