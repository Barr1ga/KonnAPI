using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class ContactCategoryRepository : IContactCategoryRepository
{
    private readonly DataContext _context;

    public ContactCategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ContactCategory?> GetContactCategory(int contactId, int categoryId)
    {
        return await _context.ContactCategories.FirstOrDefaultAsync(c => c.ContactId == contactId && c.CategoryId == categoryId);
    }

    public async Task<bool> AddContactCategory(ContactCategory contactCategory)
    {
        await _context.ContactCategories.AddAsync(contactCategory);
        return await SaveChangesAsync();
    }

    public async Task<bool> AddContactCategories(List<ContactCategory> contactCategories)
    {
        await _context.ContactCategories.AddRangeAsync(contactCategories);
        return await SaveChangesAsync();
    }

    public async Task<bool> HardDeleteContactCategory(int contactCategoryId)
    {
        var contactCategory = await _context.ContactCategories.FirstOrDefaultAsync(c => c.Id == contactCategoryId);
        if (contactCategory == null)
        {
            return false;
        }
        _context.ContactCategories.Remove(contactCategory);
        return await SaveChangesAsync();
    }
}
