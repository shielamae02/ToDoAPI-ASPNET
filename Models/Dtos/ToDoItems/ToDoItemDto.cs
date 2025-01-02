namespace ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

public class ToDoItemDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsComplete { get; init; } = false;
    public DateTime DueDate { get; init; }
}
