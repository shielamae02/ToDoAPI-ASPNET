using AutoMapper;
using ToDoAPI_ASPNET.Data;
using ToDoAPI_ASPNET.Models.Utils;
using ToDoAPI_ASPNET.Models.Config;
using ToDoAPI_ASPNET.Services.Utils;
using ToDoAPI_ASPNET.Models.Entities;
using ToDoAPI_ASPNET.Models.Response;
using ToDoAPI_ASPNET.Models.Dtos.Auth;
using ToDoAPI_ASPNET.Repositories.Auth;

namespace ToDoAPI_ASPNET.Services.Auth;

public class AuthService(
    IAuthRepository authRepository,
    IMapper mapper,
    JWTSettings jwt,
    ILogger<AuthService> logger,
    DataContext context
) : IAuthService
{
    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(AuthRegisterDto authRegister)
    {
        var validationErrors = new Dictionary<string, string>();

        if (await authRepository.IsUserExistsByCredentialAsync(authRegister.Email, authRegister.Username))
        {
            validationErrors.Add("credentials", "Invalid email address or username.");
            return ApiResponse<AuthResponseDto>.ErrorResponse(
                Error.ValidationError, Error.ErrorType.ValidationError, validationErrors
            );
        }

        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var user = mapper.Map<User>(authRegister);
            user.Password = PasswordUtil.HashPassword(authRegister.Password);

            await authRepository.AddUserAsync(user);

            var tokens = TokenUtil.GenerateTokens(user, jwt);
            await authRepository.SaveRefreshTokenAsync(
                user,
                tokens.Refresh,
                DateTime.UtcNow.AddDays(jwt.RefreshTokenExpiry),
                Token.TokenType.Refresh
            );

            await transaction.CommitAsync();

            return ApiResponse<AuthResponseDto>.SuccessResponse(Success.IS_AUTHENICATED, tokens);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "An error occurred while registering the user.");
            return ApiResponse<AuthResponseDto>.ErrorResponse(
                Error.ERROR_CREATING_RESOURCE("user"), Error.ErrorType.InternalServerError, validationErrors
            );
        }
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(AuthLoginDto authLogin)
    {
        var validationErrors = new Dictionary<string, string>();

        var user = await authRepository.GetUserByEmailAsync(authLogin.Email);

        if (user is null || !PasswordUtil.VerifyPassword(user.Password, authLogin.Password))
        {
            validationErrors.Add("user", "Invalid credentials.");
            return ApiResponse<AuthResponseDto>.ErrorResponse(
                Error.Unauthorized, Error.ErrorType.Unauthorized, validationErrors
            );
        }

        var tokens = TokenUtil.GenerateTokens(user, jwt);
        await authRepository.SaveRefreshTokenAsync(
            user,
            tokens.Refresh,
            DateTime.UtcNow.AddDays(jwt.RefreshTokenExpiry),
            Token.TokenType.Refresh
        );

        return ApiResponse<AuthResponseDto>.SuccessResponse(Success.IS_AUTHENICATED, tokens);
    }

    public async Task<bool> LogoutAsync(AuthRefreshTokenDto authRefresh)
    {
        var token = await authRepository.GetTokenByRefreshAsync(authRefresh.Token);

        if (token is null || token.isRevoked || token.ExpiresAt < DateTime.UtcNow)
            return false;

        token.isRevoked = true;
        await context.SaveChangesAsync();

        return true;
    }
}
