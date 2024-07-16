using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>?> GetAllUsers();
    Task<User?> GetUser(int? userId = null, string? email = null);
    Task<bool> SaveChangesAsync();
    Task<User> AddUser(User user);
    Task<User> UpdateUser(int userId, User user);
    // Task<User> DeleteUser(int id);
    // Task<User> RestoreUser(int id);
    // Task<User> HardDeleteUser(int id);
}
