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

        if (await authRepository.UserExistsByEmailAsync(authRegister.Email))
        {
            validationErrors.Add("emai", "Invalid email address.");
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

            var authDto = TokenUtil.GenerateTokens(user, jwt);
            await authRepository.SaveRefreshTokenAsync(user, authDto.Refresh, jwt.RefreshTokenExpiry);

            await transaction.CommitAsync();

            return ApiResponse<AuthResponseDto>.SuccessResponse(Success.IS_AUTHENICATED, authDto);
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


}
