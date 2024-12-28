
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

    public async Task<bool> UpdateRangeAsync(IEnumerable<ToDoItem> toDoItems)
    {
        if (toDoItems is null || !toDoItems.Any())
            return false;

        var itemIds = toDoItems.Select(t => t.Id).ToList();

        var existingItems = await context.ToDoItems
            .Where(t => itemIds.Contains(t.Id))
            .ToListAsync();

        if (existingItems.Count == 0)
            return false;


        foreach (var existingItem in existingItems)
        {
            var updateItem = toDoItems.FirstOrDefault(t => t.Id == existingItem.Id);

            if (updateItem != null)
            {
                existingItem.Title = updateItem.Title;
                existingItem.Description = updateItem.Description;
                existingItem.IsComplete = updateItem.IsComplete;
                existingItem.DueDate = updateItem.DueDate;
            }
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(IEnumerable<int> itemIds, bool newStatus)
    {
        var toDoItems = await context.ToDoItems
            .Where(t => itemIds.Contains(t.Id))
            .ToListAsync();

        if (toDoItems.Count == 0)
            return false;

        foreach (var todoItem in toDoItems)
        {
            todoItem.IsComplete = newStatus;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var toDoItem = await GetByIdAsync(id);
        if (toDoItem == null)
            return false;

        context.ToDoItems.Remove(toDoItem);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<int> ids)
    {
        var toDoItems = await context.ToDoItems
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();

        if (toDoItems.Count == 0)
            return false;

        context.ToDoItems.RemoveRange(toDoItems);
        await context.SaveChangesAsync();
        return true;
    }
}
