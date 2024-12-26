namespace ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

public class ToDoItemDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool isComplete { get; init; } = false;
    public DateTime DueDate { get; init; }
}
