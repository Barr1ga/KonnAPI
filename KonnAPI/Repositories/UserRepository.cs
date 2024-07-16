using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<User>?> GetAllUsers()
    {
        return await _context.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
    }

    public async Task<User?> GetUser(int? userId = null, string? email = null)
    {
        if (userId.HasValue)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
        }
        else if (!string.IsNullOrEmpty(email))
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        else
        {
            throw new ArgumentException("At least one parameter must be provided.");
        }
    }

    public async Task<User> AddUser(User user)
    {
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        await _context.Users.AddAsync(user);
        await SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateUser(int userId, User user)
    {
        user.UpdatedAt = DateTime.Now;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null || user.IsDeleted)
        {
            return false;
        }
        user.IsDeleted = true;
        user.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> RestoreUser(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null || !user.IsDeleted)
        {
            return false;
        }
        user.IsDeleted = false;
        user.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> HardDeleteUser(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null || !user.IsDeleted)
        {
            return false;
        }
        _context.Users.Remove(user);
        return await SaveChangesAsync();
    }
}
