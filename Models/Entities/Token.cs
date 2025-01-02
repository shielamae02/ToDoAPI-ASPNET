using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAPI_ASPNET.Models.Entities;

[Index(nameof(Value), IsUnique = true)]
public sealed class Token : BaseEntity
{
    public enum TokenType
    {
        Refresh,
        Access
    }

    [ForeignKey(nameof(User))]
    public int UserId { get; init; }

    public string Value { get; set; } = null!;
    public bool isRevoked { get; set; } = false;
    public DateTime ExpiresAt { get; init; }
    public TokenType Type { get; set; } = TokenType.Refresh;

    public User User { get; init; } = null!;
}
