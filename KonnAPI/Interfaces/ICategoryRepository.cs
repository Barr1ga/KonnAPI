using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface ICategoryRepository {
    Task<IEnumerable<Category>> GetAllCategories();
    Task<IEnumerable<Category>> GetWorkspaceCategories(int id);
}
