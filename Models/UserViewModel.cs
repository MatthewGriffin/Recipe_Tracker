using recipe_tracker.Database.Models;

namespace recipe_tracker.Models;

public class UserViewModel
{
    public string Username { get; private init; } = "";

    public static implicit operator UserViewModel(User v)
    {
        return new UserViewModel
        {
            Username = v.Username
        };
    }
}