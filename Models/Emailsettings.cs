namespace recipe_tracker.Models;

public class EmailSettings
{
    public required string Host { get; init; }
    public int Port { get; init; }
    public required string From { get; init; }
    public required string Password { get; init; }
}