using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAPI_ASPNET.Models.Entities;

[Index(nameof(Value), IsUnique = true)]
public sealed class Token : BaseEntity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
    public string Value { get; set; } = null!;
    public bool isRevoked { get; set; } = false;
    public DateTime Expiration { get; init; }

    public User User { get; init; } = null!;
}
