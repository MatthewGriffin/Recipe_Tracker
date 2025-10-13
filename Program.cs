using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using recipe_tracker.Database;
using recipe_tracker.Models;
using recipe_tracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RecipeTrackerContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RecipeTrackerContext>().AddDefaultTokenProviders();
builder.Services.AddResponseCaching();
builder.Services.AddOptions();
builder.Services.Configure<EmailSettings>(builder.Configuration)
    .AddSingleton(sp => sp.GetRequiredService<IOptions<EmailSettings>>().Value);

builder.Services.AddScoped<RoleSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Auto apply pending migrations on start (this way new deployments auto create db structure)
await using (var context = new RecipeTrackerContext())
{
    context.Database.Migrate();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

// Seed roles when the application starts
using (var scope = app.Services.CreateScope())
{
    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
    await roleSeeder.SeedRolesAsync();
}

app.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();