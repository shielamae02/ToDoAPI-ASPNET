namespace ToDoAPI_ASPNET.Models.Dtos.Auth;

public class AuthResponseDto
{
    public string Access { get; init; } = string.Empty;
    public string Refresh { get; init; } = string.Empty;
}
