

using AutoMapper;
using ToDoAPI_ASPNET.Models.Utils;
using ToDoAPI_ASPNET.Models.Entities;
using ToDoAPI_ASPNET.Models.Response;
using ToDoAPI_ASPNET.Models.Dtos.ToDoItems;
using ToDoAPI_ASPNET.Repositories.ToDoItems;

namespace ToDoAPI_ASPNET.Services.ToDoItems;

public class ToDoItemService(
    IToDoItemRepository toDoItemRepository,
    IMapper mapper
) : IToDoItemService
{
    public async Task<ApiResponse<ToDoItemDto>> CreateItemAsync(int userId, ToDoItemCreateDto toDoItem)
    {
        var newToDoItem = mapper.Map<ToDoItem>(toDoItem);
        newToDoItem.UserId = userId;

        await toDoItemRepository.CreateAsync(newToDoItem);

        return ApiResponse<ToDoItemDto>.SuccessResponse(
            Success.RESOURCE_CREATED("ToDoItem"),
            mapper.Map<ToDoItemDto>(newToDoItem)
        );
    }

    public async Task<ApiResponse<ToDoItemDto>> GetToDoItemByIdAsync(int userId, int itemId)
    {
        var toDoItem = await toDoItemRepository.GetByIdAsync(itemId);
        if (toDoItem is null || toDoItem.UserId != userId)
            return ApiResponse<ToDoItemDto>.ErrorResponse(
               Error.NotFound,
               Error.ErrorType.NotFound,
               new Dictionary<string, string> { { "toDoItem", $"Item with id {itemId} does not exist." } }
           );

        return ApiResponse<ToDoItemDto>.SuccessResponse(
           Success.RESOURCE_RETRIEVED("ToDoItem"),
           mapper.Map<ToDoItemDto>(toDoItem)
       );
    }

    

}
