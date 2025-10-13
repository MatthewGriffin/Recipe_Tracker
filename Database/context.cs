using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using recipe_tracker.Database.Models;

namespace recipe_tracker.Database;

public class RecipeTrackerContext : DbContext
{
    private readonly IEncryptionProvider _encryptionProvider;

    public RecipeTrackerContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        path = Path.Join(path, "RecipeTracker");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        DbPath = Path.Join(path, "RecipeTracker.db");

        _encryptionProvider = new GenerateEncryptionProvider("IjV7Zm2lvcBicJ2AmeSWlkxZCNxY8Bjp");
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeImage> RecipeImages { get; set; }
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseEncryption(_encryptionProvider);
    }
}