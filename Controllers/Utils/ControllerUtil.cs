using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI_ASPNET.Models.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static ToDoAPI_ASPNET.Models.Utils.Error;

namespace ToDoAPI_ASPNET.Controllers.Utils;

public static class ControllerUtil
{
    public static int GetUserId(ClaimsPrincipal user)
    {
        var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(userIdString, out var userId) ? userId : -1;
    }
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

    public static ApiResponse<T> GenerateValidationError<T>(ModelStateDictionary modelState)
    {
        var validationErrors = modelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => char.ToLower(kvp.Key[0]) + kvp.Key[1..],
                kvp => string.Join("; ", kvp.Value.Errors.Select(e => e.ErrorMessage))
            );

        return ApiResponse<T>.ErrorResponse(
            "Validation failed.",
            ErrorType.ValidationError,
            validationErrors
        );
    }
}
