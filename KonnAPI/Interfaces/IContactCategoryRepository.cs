using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IContactCategoryRepository
{
    Task<bool> SaveChangesAsync();
    Task<ContactCategory?> GetContactCategory(int contactId, int categoryId);
    Task<bool> AddContactCategory(ContactCategory contactCategory);
    Task<bool> AddContactCategories(List<ContactCategory> contactCategories);
    Task<bool> HardDeleteContactCategory(int contactCategoryId);
}
