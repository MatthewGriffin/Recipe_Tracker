using Microsoft.EntityFrameworkCore;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Database.Repositories
{
    public class RecipeTrackerContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeImage> RecipeImages { get; set; }

        public string DbPath { get; }

        public RecipeTrackerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            path = Path.Join(path, "RecipeTracker");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DbPath = Path.Join(path, "RecipeTracker.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}