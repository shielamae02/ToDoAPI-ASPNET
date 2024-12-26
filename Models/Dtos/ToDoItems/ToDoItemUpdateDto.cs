namespace ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

public class ToDoItemUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool isComplete { get; set; }
    public DateTime DueDate { get; set; }
}
