using ToDoAPI_ASPNET.Data;
using Microsoft.EntityFrameworkCore;
using ToDoAPI_ASPNET.Models.Entities;

namespace ToDoAPI_ASPNET.Repositories.Auth
{
    public class AuthRepository(
        DataContext context
    ) : IAuthRepository
    {
        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task SaveRefreshTokenAsync(User user, string refreshToken, DateTime expiresAt, Token.TokenType type)
        {
            var token = new Token
            {
                User = user,
                UserId = user.Id,
                Value = refreshToken,
                ExpiresAt = expiresAt,
                Type = type
            };

            user.Tokens.Add(token);
            await context.Tokens.AddAsync(token);
            await context.SaveChangesAsync();
        }

        public async Task<Token?> GetTokenByRefreshAsync(string refreshToken)
        {
            return await context.Tokens.SingleOrDefaultAsync(t => t.Value == refreshToken);
        }
    }
}