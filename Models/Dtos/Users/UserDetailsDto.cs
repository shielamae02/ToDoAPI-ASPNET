using ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

namespace ToDoAPI_ASPNET.Models.Dtos.Users;

public class UserDetailsDto
{
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    public ICollection<ToDoItemDto> ToDoItems { get; init; } = new List<ToDoItemDto>();
}
