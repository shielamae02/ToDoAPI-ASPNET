namespace ToDoAPI_ASPNET.Services.Utils;

public class PasswordUtil
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
