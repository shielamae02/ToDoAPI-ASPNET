using ToDoAPI_ASPNET.Models.Entities;

namespace ToDoAPI_ASPNET.Repositories.ToDoItems;

public interface IToDoItemRepository
{
    Task<ToDoItem> CreateAsync(ToDoItem toDoItem);
    Task<ToDoItem?> GetByIdAsync(int id);
    Task<IEnumerable<ToDoItem>> GetAllAsync(int userId);
    Task<bool> UpdateAsync(ToDoItem toDoItem);
    Task<bool> UpdateStatusAsync(IEnumerable<int> itemIds, bool newStatus);
    Task<bool> UpdateRangeAsync(IEnumerable<ToDoItem> toDoItems);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteRangeAsync(IEnumerable<int> ids);
    hello
}

