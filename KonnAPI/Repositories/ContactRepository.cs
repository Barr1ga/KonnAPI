using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class ContactRepository : IContactRepository {
    private readonly DataContext _context;

    public ContactRepository(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllContacts() {
        return await _context.Contacts.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetWorkspaceContacts(int id) {
        return await _context.Contacts.Where(a => a.WorkspaceId == id).OrderByDescending(a => a.Id).ToListAsync();
    }
}
