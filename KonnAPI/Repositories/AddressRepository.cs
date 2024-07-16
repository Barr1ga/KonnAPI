using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly DataContext _context;

    public AddressRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Address>> GetAllAddresses()
    {
        return await _context.Addresses.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetContactAddresses(int id)
    {
        return await _context.Addresses.Where(a => a.ContactId == id).OrderByDescending(a => a.Id).ToListAsync();
    }

    public async Task<bool> AddAddress(int contactId, Address address)
    {
        address.ContactId = contactId;
        address.CreatedAt = DateTime.Now;
        address.UpdatedAt = DateTime.Now;
        await _context.Addresses.AddAsync(address);
        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateAddress(int addressId, Address address)
    {
        address.UpdatedAt = DateTime.Now;
        _context.Addresses.Update(address);
        return await SaveChangesAsync();
    }
}
