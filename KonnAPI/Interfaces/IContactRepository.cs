using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IContactRepository
{
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Contact>> GetAllContacts();
    Task<IEnumerable<Contact>> GetWorkspaceContacts(int workspaceId);
    Task<Contact?> GetContact(int? contactId = null, string? name = null, string? email = null);
    Task<bool> AddContact(int workspaceId, Contact contact);
    Task<bool> UpdateContact(int contactId, Contact contact);
    Task<bool> DeleteContact(int contactId);
    Task<bool> RestoreContact(int contactId);
    Task<bool> HardDeleteContact(int contactId);
}
