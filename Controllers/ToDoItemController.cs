using Microsoft.AspNetCore.Mvc;
using ToDoAPI_ASPNET.Models.Utils;
using ToDoAPI_ASPNET.Controllers.Utils;
using ToDoAPI_ASPNET.Services.ToDoItems;
using ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

namespace ToDoAPI_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/toDoItems")]
public class ToDoItemController(
    IToDoItemService toDoItemService,
    ILogger<ToDoItemController> logger
) : ControllerBase
{
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> CreateToDoItem([FromBody] ToDoItemCreateDto toDoItem)
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
                return Unauthorized(new { message = Error.Unauthorized });

            if (!ModelState.IsValid)
                return BadRequest(ControllerUtil.GenerateValidationError<object>(ModelState));

            var response = await toDoItemService.CreateItemAsync(userId, toDoItem);
            if (response.Status.Equals("error"))
            {
                return ControllerUtil.GetActionResultFromError(response);
            }

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while creating the to-do item.");
            return Problem("An unexpected error occurred while creating the to-do item.");
        }
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetToDoItemById([FromRoute] int id)
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
                return Unauthorized(new { message = Error.Unauthorized });

            var response = await toDoItemService.GetToDoItemByIdAsync(userId, id);
            if (response.Status.Equals("error"))
            {
                return ControllerUtil.GetActionResultFromError(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while retrieving the to-do item.");
            return Problem("An unexpected error occurred while retrieving the to-do item.");
        }
    }


    
}
