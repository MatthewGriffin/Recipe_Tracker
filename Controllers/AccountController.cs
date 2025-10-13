using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using recipe_tracker.Helpers;
using recipe_tracker.Models;

namespace recipe_tracker.Controllers;

public class AccountController(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IOptions<EmailSettings> emailSettings)
    : Controller
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel { Email = "", Password = "" });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel regUser)
    {
        if (!ModelState.IsValid) return View(regUser);

        var user = new IdentityUser
        {
            UserName = regUser.Email,
            Email = regUser.Email
        };

        var result = await userManager.CreateAsync(user, regUser.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(regUser);
        }

        await userManager.AddToRoleAsync(user, "User");

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.UserName, token },
            Request.Scheme);
        var body = $"Welcome to the recipe tracker {user.UserName}.<br/><br/><a href='" + callbackUrl +
                   "'>Please click here to confirm your account!.</a>";

        var sender = new EmailSender(_emailSettings.Host, _emailSettings.From, _emailSettings.Password,
            _emailSettings.Port,
            user.UserName
            , body, "Please confirm your account for recipe tracker!");
        sender.SendEmail();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await userManager.FindByEmailAsync(userId);
        if (user == null) return RedirectToAction("Index", "Home");

        var tokenTest = await userManager.ConfirmEmailAsync(user, token);
        if (tokenTest.Succeeded) return RedirectToAction("Index", "Home");
        {
            await signInManager.SignInAsync(user, false);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginUser)
    {
        if (!ModelState.IsValid) return View(loginUser);
        var result = await signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password,
            false, false);

        if (result.Succeeded) return RedirectToAction("Index", "Home");

        ModelState.AddModelError("",
            result.IsNotAllowed ? "Email must be verified before login" : "Invalid login attempt");

        return View(loginUser);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}