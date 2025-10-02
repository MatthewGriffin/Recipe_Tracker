using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_tracker.Database.Repositories;
using recipe_tracker.Models;

namespace recipe_tracker.Controllers;

public class HomeController(ILogger<HomeController> logger, RecipeTrackerContext dbContext) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var recipies = dbContext.Recipes.Include(r => r.RecipeDetails).Include(r => r.RecipeDetails.Image)
                                      .Include(r => r.Instructions).Include(r => r.Ingredients)
                                      .Select(r => r.ToRecipeViewModel()).ToList();
        return View(recipies);
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult ViewRecipe(int recipeId)
    {
        logger.LogInformation("Requesting recipe {id}", recipeId);
        var recipe = dbContext.Recipes.Include(r => r.RecipeDetails).Include(r => r.RecipeDetails.Image)
                                      .Include(r => r.Instructions).Include(r => r.Ingredients)
                                      .First(recipe => recipe.RecipeID == recipeId);
        if (recipe.RecipeDetails == null)
        {
            logger.LogWarning("Requesting recipe {id} failed returning to main page", recipeId);
            return View("Index");
        }
        var vm = recipe.ToRecipeViewModel();
        logger.LogInformation("Recipe {id} found loading view page", recipeId);
        return View(vm);
    }

    [HttpGet]
    public IActionResult AddRecipe()
    {
        return View(new RecipeViewModel());
    }

    [HttpPost]
    public IActionResult AddRecipe(RecipeViewModel recipe)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("AddRecipeViewModel is not valid aborting");
            return View(recipe);
        }

        var dbRecipe = recipe.ToRecipeDBModel();
        logger.LogInformation("{Title} recipe is valid saving to db", dbRecipe.RecipeDetails.Title);
        dbContext.Recipes.Add(dbRecipe);
        dbContext.SaveChanges();
        logger.LogInformation("Redirecting user to view recipe page");
        return View("ViewRecipe", dbRecipe);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
