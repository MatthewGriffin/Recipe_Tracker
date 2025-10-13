using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.EncryptColumn.Attribute;
using recipe_tracker.Models;

namespace recipe_tracker.Database.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [MaxLength(50)] public required string Username { get; set; }

    [MaxLength(50)] [EncryptColumn] public string Password { get; set; } = "";
    [MaxLength(50)] [EncryptColumn] public string Email { get; set; } = "";

    public static implicit operator User(UserViewModel v)
    {
        return new User
        {
            Username = v.Username
        };
    }
}