using Microsoft.EntityFrameworkCore;

namespace ToDoAPI_ASPNET.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
public sealed class User : BaseEntity
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Password { get; set; } = null!;

    public ICollection<ToDoItem> ToDoItems { get; init; } = new List<ToDoItem>();
    public ICollection<Token> Tokens { get; init; } = new List<Token>();
}
