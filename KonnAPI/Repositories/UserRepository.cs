using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class UserRepository : IUserRepository {
    private readonly DataContext _context;

    public UserRepository(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers() {
        return await _context.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
    }
}
