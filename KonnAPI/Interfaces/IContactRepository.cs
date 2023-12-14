using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IContactRepository {
    Task<IEnumerable<Contact>> GetAllContacts();
    Task<IEnumerable<Contact>> GetWorkspaceContacts(int id);
}
