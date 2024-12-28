
using ToDoAPI_ASPNET.Data;
using Microsoft.EntityFrameworkCore;
using ToDoAPI_ASPNET.Models.Entities;

namespace ToDoAPI_ASPNET.Repositories.ToDoItems;

public class ToDoItemRepository(
    DataContext context
    ) : IToDoItemRepository
{
    public async Task<ToDoItem> CreateAsync(ToDoItem toDoItem)
    {
        await context.AddAsync(toDoItem);
        await context.SaveChangesAsync();

        return toDoItem;
    }

    
}
