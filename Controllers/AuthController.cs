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

}
