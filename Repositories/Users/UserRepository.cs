using ToDoAPI_ASPNET.Data;
using Microsoft.EntityFrameworkCore;
using ToDoAPI_ASPNET.Models.Entities;

namespace ToDoAPI_ASPNET.Repositories.Users;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users
            .Include(u => u.ToDoItems)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
