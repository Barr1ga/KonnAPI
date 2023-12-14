using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class AddressRepository : IAddressRepository {
    private readonly DataContext _context;

    public AddressRepository(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Address>> GetAllAddresses() {
        return await _context.Addresses.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetContactAddresses(int id) {
        return await _context.Addresses.Where(a => a.ContactId == id).OrderByDescending(a => a.Id).ToListAsync();
    }
}
