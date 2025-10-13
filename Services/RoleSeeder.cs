using Microsoft.AspNetCore.Identity;

namespace recipe_tracker.Services;

public class RoleSeeder(RoleManager<IdentityRole> roleManager)
{
    public async Task SeedRolesAsync()
    {
        string[] roles = ["User"];
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
    }
}