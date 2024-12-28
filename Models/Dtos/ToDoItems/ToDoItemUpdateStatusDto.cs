using System.ComponentModel.DataAnnotations;

namespace ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

public class ToDoItemUpdateStatusDto
{
    [Required(ErrorMessage = "To-do item IDs are required.")]
    [MinLength(1, ErrorMessage = "Item Ids must contain at least one value.")]
    public IList<int> ItemIds { get; set; } = new List<int>();


    [Required(ErrorMessage = "New status is required.")]
    public bool NewStatus { get; set; } = true;
}
