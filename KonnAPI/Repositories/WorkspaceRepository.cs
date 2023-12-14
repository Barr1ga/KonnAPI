using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class WorkspaceRepository : IWorkspaceRepository {
    private readonly DataContext _context;

    public WorkspaceRepository(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Workspace>> GetAllWorkspaces() {
        return await _context.Workspaces.OrderByDescending(w => w.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Workspace>> GetUserWorkspaces(int id) {
        return await _context.Workspaces.Where(w => w.UserId).OrderByDescending(w => w.CreatedAt).ToListAsync();
    }
}
