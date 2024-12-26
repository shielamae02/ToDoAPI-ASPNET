using System.ComponentModel.DataAnnotations;

namespace ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

public class ToDoItemCreateDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
    public string Title { get; init; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 100 characters.")]
    public string Description { get; init; } = string.Empty;

    public bool isComplete { get; init; } = false;

    public DateTime DueDate { get; init; }
}
