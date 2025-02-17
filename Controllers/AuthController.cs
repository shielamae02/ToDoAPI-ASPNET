using Microsoft.AspNetCore.Mvc;
using ToDoAPI_ASPNET.Services.Auth;
using ToDoAPI_ASPNET.Models.Dtos.Auth;
using ToDoAPI_ASPNET.Controllers.Utils;

namespace ToDoAPI_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(
    IAuthService authService,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> RegisterUser([FromBody] AuthRegisterDto authRegister)
    {
        if (!ModelState.IsValid)
            return BadRequest(ControllerUtil.GenerateValidationError<AuthResponseDto>(ModelState));

        try
        {
            var response = await authService.RegisterAsync(authRegister);

            if (response.Status.Equals("error"))
            {
                logger.LogWarning("Registration attempt failed for email: {email}.", authRegister.Email);
                return ControllerUtil.GetActionResultFromError(response);
            }

            logger.LogInformation("Registration successful for email: {email}.", authRegister.Email);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Registration failed unexpectedlyu for email: {email}.", authRegister.Email);
            return Problem("An error occurred while processing your request.");
        }
    }

    [HttpPost("login")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> LoginUser([FromBody] AuthLoginDto authLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ControllerUtil.GenerateValidationError<AuthResponseDto>(ModelState));

        try
        {
            var response = await authService.LoginAsync(authLogin);

            if (response.Status.Equals("error"))
            {
                logger.LogWarning("Login failed for email: {email}.", authLogin.Email);
                return ControllerUtil.GetActionResultFromError(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Login failed unexpectedly for email: {email}.", authLogin.Email);
            return Problem("An error occurred while processing your request.");
        }
    }

    [HttpPost("logout")]
    [Consumes("application/json")]
    public async Task<IActionResult> LogoutUser([FromBody] AuthRefreshTokenDto authRefresh)
    {
        try
        {
            var response = await authService.LogoutAsync(authRefresh);

            if (!response)
            {
                logger.LogWarning("Logout failed.");
                return BadRequest();
            }

            logger.LogInformation("User successfully logged out.");
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Error in logging out user.");
            return Problem("An error occurred while processing your request.");
        }
    }

}
