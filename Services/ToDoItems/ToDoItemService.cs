

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

    public async Task<ApiResponse<IEnumerable<ToDoItemDto>>> GetAllItemsAsync(int userId)
    {
        var toDoItems = await toDoItemRepository.GetAllAsync(userId);

        var result = toDoItems.Select(mapper.Map<ToDoItemDto>).ToList();

        return ApiResponse<IEnumerable<ToDoItemDto>>.SuccessResponse(
           Success.RESOURCE_RETRIEVED("ToDoItems"),
           result
       );
    }

    public async Task<ApiResponse<ToDoItemDto>> UpdateToDoItemAsync(int userId, int itemId, ToDoItemUpdateDto toDoItem)
    {
        var toDoItemEntity = await toDoItemRepository.GetByIdAsync(itemId);
        if (toDoItemEntity is null || toDoItemEntity.UserId != userId)
            return ApiResponse<ToDoItemDto>.ErrorResponse(
                Error.NotFound,
                Error.ErrorType.NotFound,
                new Dictionary<string, string> { { "toDoItem", $"Item with id {itemId} does not exist." } }
            );

        mapper.Map(toDoItem, toDoItemEntity);

        var isUpdateSuccess = await toDoItemRepository.UpdateAsync(toDoItemEntity);
        if (!isUpdateSuccess)
            return ApiResponse<ToDoItemDto>.ErrorResponse(
                Error.OperationFailed,
                Error.ErrorType.InternalServerError
            );


        return ApiResponse<ToDoItemDto>.SuccessResponse(
            Success.RESOURCE_UPDATED("ToDoItem"),
            mapper.Map<ToDoItemDto>(toDoItemEntity)
        );
    }

    public async Task<ApiResponse<object>> DeleteToDoItemAsync(int userId, int itemId)
    {
        var toDoItem = await toDoItemRepository.GetByIdAsync(itemId);
        if (toDoItem is null || toDoItem.UserId != userId)
            return ApiResponse<object>.ErrorResponse(
                Error.NotFound,
                Error.ErrorType.NotFound,
                new Dictionary<string, string> { { "toDoItem", $"Item with id {itemId} does not exist." } }
            );


        var isDeleteSuccess = await toDoItemRepository.DeleteAsync(itemId);
        if (!isDeleteSuccess)
            return ApiResponse<object>.ErrorResponse(
                Error.OperationFailed,
                Error.ErrorType.InternalServerError
            );

        return ApiResponse<object>.SuccessResponse(
            Success.RESOURCE_DELETED("ToDoItem"),
            null
        );
    }

    public async Task<ApiResponse<object>> UpdateToDoItemsStatusAsync(int userId, ToDoItemUpdateStatusDto toDoItem)
    {
        var validationErrors = new Dictionary<string, string>();

        foreach (var itemId in toDoItem.ItemIds)
        {
            var item = await toDoItemRepository.GetByIdAsync(itemId);
            if (item is null || item.UserId != userId)
            {
                validationErrors.Add("toDoItem", $"Item with id {itemId} does not exist.");
                return ApiResponse<object>.ErrorResponse(
                    Error.NotFound,
                    Error.ErrorType.NotFound,
                    validationErrors
                );
            }
        }

        var isUpdateSuccess = await toDoItemRepository.UpdateStatusAsync(toDoItem.ItemIds, toDoItem.NewStatus);
        if (!isUpdateSuccess)
            return ApiResponse<object>.ErrorResponse(
                Error.OperationFailed,
                Error.ErrorType.InternalServerError
            );

        return ApiResponse<object>.SuccessResponse(
            Success.RESOURCE_UPDATED("ToDoItems"),
            null
        );
    }





}
