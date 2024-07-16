using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface ICategoryRepository
{
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Category>> GetAllCategories();
    Task<IEnumerable<Category>> GetWorkspaceCategories(int id);
    Task<bool> AddCategory(int workspaceId, Category category);
    Task<bool> UpdateCategory(int categoryId, Category category);
    Task<bool> DeleteCategory(int categoryId);
    Task<bool> RestoreCategory(int categoryId);
    Task<bool> HardDeleteCategory(int categoryId);
}
