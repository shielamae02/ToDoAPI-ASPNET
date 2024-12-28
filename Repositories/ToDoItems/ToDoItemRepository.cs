
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

    public async Task<ToDoItem?> GetByIdAsync(int id)
    {
        return await context.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<ToDoItem>> GetAllAsync(int userId)
    {
        return await context.ToDoItems
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(ToDoItem toDoItem)
    {
        var existingItem = await GetByIdAsync(toDoItem.Id);
        if (existingItem is null)
            return false;

        existingItem.Title = toDoItem.Title;
        existingItem.Description = toDoItem.Description;
        existingItem.IsComplete = toDoItem.IsComplete;
        existingItem.DueDate = toDoItem.DueDate;

        await context.SaveChangesAsync();
        return true;
    }



}
