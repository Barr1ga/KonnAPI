using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly DataContext _context;

    public ContactRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Contact>> GetAllContacts()
    {
        return await _context.Contacts.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetWorkspaceContacts(int id)
    {
        return await _context.Contacts.Where(a => a.WorkspaceId == id).OrderByDescending(a => a.Id).ToListAsync();
    }

    public async Task<bool> AddContact(int workspaceId, Contact contact)
    {
        contact.WorkspaceId = workspaceId;
        contact.CreatedAt = DateTime.Now;
        contact.UpdatedAt = DateTime.Now;
        await _context.Contacts.AddAsync(contact);
        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateContact(int contactId, Contact contact)
    {
        contact.UpdatedAt = DateTime.Now;
        _context.Contacts.Update(contact);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteContact(int contactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId);
        if (contact == null)
        {
            return false;
        }

        contact.IsDeleted = true;
        contact.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> RestoreContact(int contactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId);
        if (contact == null)
        {
            return false;
        }

        contact.IsDeleted = false;
        contact.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> HardDeleteContact(int contactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId);
        if (contact == null || !contact.IsDeleted)
        {
            return false;
        }

        _context.Contacts.Remove(contact);
        return await SaveChangesAsync();
    }
}
