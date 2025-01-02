using Microsoft.AspNetCore.Mvc;
using ToDoAPI_ASPNET.Services.Users;
using ToDoAPI_ASPNET.Controllers.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ToDoAPI_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController(
    IUserService userService,
    ILogger<UserController> logger
) : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserDetails()
    {
        var userId = ControllerUtil.GetUserId(User);

        if (userId == -1)
        {
            logger.LogWarning("Unauthorized access attempt detected for user ID: {userId}.", userId);
            return Unauthorized();
        }

        try
        {
            var response = await userService.GetUserByIdAsync(userId);

            logger.LogInformation("Successfully fetched user details for user ID: {userID}.", userId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Failed to fetch user details.");
            return Problem("An error occurred while processing your request.");
        }
    }
}
