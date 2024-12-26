using ToDoAPI_ASPNET.Models.Response;
using ToDoAPI_ASPNET.Models.Dtos.Auth;

namespace ToDoAPI_ASPNET.Services.Auth;

public interface IAuthService
{
    public Task<ApiResponse<AuthResponseDto>> RegisterAsync(AuthRegisterDto authRegister);
    public Task<ApiResponse<AuthResponseDto>> LoginAsync(AuthLoginDto authLogin);
    public Task<bool> LogoutAsync(AuthRefreshTokenDto authRefresh);
}
