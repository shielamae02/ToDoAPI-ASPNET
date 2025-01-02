using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAPI_ASPNET.Models.Entities;

public sealed class ToDoItem : BaseEntity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsComplete { get; set; } = false;
    public DateTime? DueDate { get; set; }

    public User User { get; init; } = null!;
}
