using ToDoAPI_ASPNET.Models.Entities;

namespace ToDoAPI_ASPNET.Repositories.Auth;

public interface IAuthRepository
{
    Task<bool> IsUserExistsByCredentialAsync(string email, string? username = null);
    Task<User?> GetUserByEmailAsync(string email);
    Task<Token?> GetTokenByRefreshAsync(string refreshToken);
    Task AddUserAsync(User user);
    Task SaveRefreshTokenAsync(User user, string refreshToken, DateTime expiresAt, Token.TokenType type);
}
