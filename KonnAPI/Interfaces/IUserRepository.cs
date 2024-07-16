using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>?> GetAllUsers();
    Task<User?> GetUser(int? userId = null, string? email = null);
    Task<bool> SaveChangesAsync();
    Task<User> AddUser(User user);
    Task<bool> UpdateUser(int userId, User user);
    Task<bool> DeleteUser(int id);
    Task<bool> RestoreUser(int id);
    Task<bool> HardDeleteUser(int id);
}
