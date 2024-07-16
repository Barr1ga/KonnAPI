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
        return await _context.Contacts.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetWorkspaceContacts(int workspaceId)
    {
        return await _context.Contacts.Where(c => c.WorkspaceId == workspaceId).OrderByDescending(c => c.Id).ToListAsync();
    }

    public async Task<User?> GetContact(int? contactId = null, string? name = null, string? email = null)
    {
        if (contactId.HasValue)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == contactId);
        }
        else if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Name == name && c.Email == email);
        }
        else
        {
            throw new ArgumentException("At least one parameter must be provided.");
        }
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
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
        if (contact == null || contact.IsDeleted)
        {
            return false;
        }

        contact.IsDeleted = true;
        contact.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> RestoreContact(int contactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
        if (contact == null || !contact.IsDeleted)
        {
            return false;
        }
        contact.IsDeleted = false;
        contact.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> HardDeleteContact(int contactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
        if (contact == null || !contact.IsDeleted)
        {
            return false;
        }
        _context.Contacts.Remove(contact);
        return await SaveChangesAsync();
    }
}
