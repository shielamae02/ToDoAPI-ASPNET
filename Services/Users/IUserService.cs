using ToDoAPI_ASPNET.Models.Response;
using ToDoAPI_ASPNET.Models.Dtos.Users;

namespace ToDoAPI_ASPNET.Services.Users;

public interface IUserService
{
    public Task<ApiResponse<UserDetailsDto>> GetUserByIdAsync(int id);
}
