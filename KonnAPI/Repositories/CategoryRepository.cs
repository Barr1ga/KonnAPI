using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetWorkspaceCategories(int id)
    {
        return await _context.Categories.Where(c => c.WorkspaceId == id).OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<bool> AddCategory(int workspaceId, Category category)
    {
        category.WorkspaceId = workspaceId;
        category.CreatedAt = DateTime.Now;
        category.UpdatedAt = DateTime.Now;
        await _context.Categories.AddAsync(category);
        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateCategory(int categoryId, Category category)
    {
        category.UpdatedAt = DateTime.Now;
        _context.Categories.Update(category);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteCategory(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null || category.IsDeleted)
        {
            return false;
        }

        category.IsDeleted = true;
        category.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> RestoreCategory(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null || !category.IsDeleted)
        {
            return false;
        }
        category.IsDeleted = false;
        category.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> HardDeleteCategory(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null || category.IsDeleted)
        {
            return false;
        }
        _context.Categories.Remove(category);
        return await SaveChangesAsync();
    }
}
