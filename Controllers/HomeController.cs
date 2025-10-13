using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_tracker.Database;
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
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}