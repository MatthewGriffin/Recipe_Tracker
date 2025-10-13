using System.ComponentModel.DataAnnotations;

namespace recipe_tracker.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please enter email.")]
    [EmailAddress]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Please enter password.")]
    public required string Password { get; init; }

    public bool IsValid { get; init; } = true;
}