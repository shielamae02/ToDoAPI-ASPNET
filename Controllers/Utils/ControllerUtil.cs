using Microsoft.AspNetCore.Mvc;
using ToDoAPI_ASPNET.Models.Response;
using static ToDoAPI_ASPNET.Models.Utils.Error;

namespace ToDoAPI_ASPNET.Controllers.Utils;

public static class ControllerUtil
{
    public static IActionResult GetActionResultFromError<T>(ApiResponse<T> apiResponse)
    {
        var errorType = apiResponse.ErrorType;

        return errorType switch
        {
            ErrorType.BadRequest => new BadRequestObjectResult(apiResponse),
            ErrorType.NotFound => new NotFoundObjectResult(apiResponse),
            ErrorType.ValidationError => new BadRequestObjectResult(apiResponse),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(apiResponse),
            ErrorType.InternalServerError => new StatusCodeResult(500),
            _ => new BadRequestObjectResult(apiResponse)
        };
    }
}
