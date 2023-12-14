using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IUserRepository {
    Task<IEnumerable<User>> GetAllUsers();
}
