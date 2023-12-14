using KonnAPI.Data;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class UserRepository {
    private readonly DataContext _context;

    public UserRepository(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers() {
        return await _context.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
    }
}
