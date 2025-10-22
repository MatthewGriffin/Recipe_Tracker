using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Database;

public class RecipeTrackerContext : IdentityDbContext<IdentityUser>
{
    public RecipeTrackerContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        path = Path.Join(path, "RecipeTracker");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        DbPath = Path.Join(path, "RecipeTracker.db");
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeImage> RecipeImages { get; set; }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}