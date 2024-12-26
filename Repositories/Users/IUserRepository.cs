using ToDoAPI_ASPNET.Models.Entities;

namespace ToDoAPI_ASPNET.Repositories.Users;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(int id);
}
