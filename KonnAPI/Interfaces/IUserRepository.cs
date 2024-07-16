using KonnAPI.Dto;
using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IUserRepository
{
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<User>?> GetAllUsers();
    Task<User?> GetUser(int? userId = null, string? email = null);
    Task<bool> AddUser(User user);
    Task<bool> UpdateUser(int userId, User user);
    Task<bool> DeleteUser(int userId);
    Task<bool> RestoreUser(int userId);
    Task<bool> HardDeleteUser(int userId);
}
