using AutoMapper;
using ToDoAPI_ASPNET.Models.Utils;
using ToDoAPI_ASPNET.Models.Response;
using ToDoAPI_ASPNET.Models.Dtos.Users;
using ToDoAPI_ASPNET.Repositories.Users;

namespace ToDoAPI_ASPNET.Services.Users;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper
) : IUserService
{
    public async Task<ApiResponse<UserDetailsDto>> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);

        var userDetails = mapper.Map<UserDetailsDto>(user);

        return ApiResponse<UserDetailsDto>.SuccessResponse(Success.RESOURCE_RETRIEVED("User"), userDetails);
    }
}
