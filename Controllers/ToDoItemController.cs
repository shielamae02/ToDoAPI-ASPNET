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
   
}
