using ToDoAPI_ASPNET.Models.Response;
using ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

namespace ToDoAPI_ASPNET.Services.ToDoItems;

public interface IToDoItemService
{
    Task<ApiResponse<ToDoItemDto>> CreateItemAsync(int userId, ToDoItemCreateDto toDoItem);
    Task<ApiResponse<ToDoItemDto>> GetToDoItemByIdAsync(int userId, int itemId);
    Task<ApiResponse<IEnumerable<ToDoItemDto>>> GetAllItemsAsync(int userId);
    Task<ApiResponse<ToDoItemDto>> UpdateToDoItemAsync(int userId, int itemId, ToDoItemUpdateDto toDoItem);
    Task<ApiResponse<object>> UpdateToDoItemsStatusAsync(int userId, ToDoItemUpdateStatusDto toDoItem);
    Task<ApiResponse<object>> DeleteToDoItemAsync(int userId, int itemId);
    Task<ApiResponse<object>> DeleteToDoItemsAsync(int userId, IList<int> itemIds);

}
