using System.ComponentModel;
using System.Diagnostics;
using KonnAPI.Data;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

namespace KonnAPI.Repositories;

public class WorkspaceRepository : IWorkspaceRepository
{
    private readonly DataContext _context;

    public WorkspaceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Workspace>> GetAllWorkspaces()
    {
        return await _context.Workspaces.OrderByDescending(w => w.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Workspace>> GetUserWorkspaces(int userId)
    {
        return await _context.Workspaces.Where(w => w.UserId == userId).OrderByDescending(w => w.CreatedAt).ToListAsync();
    }

    public async Task<Workspace?> GetUserWorkspace(int userId, int? workspaceId = null, string? workspaceName = null)
    {
        if (workspaceId.HasValue)
        {
            return await _context.Workspaces.FirstOrDefaultAsync(u => u.UserId == userId && u.Id == workspaceId);
        }
        else if (!string.IsNullOrEmpty(workspaceName))
        {
            return await _context.Workspaces.FirstOrDefaultAsync(u => u.UserId == userId && u.Name == workspaceName);
        }
        else
        {
            throw new ArgumentException("At least one parameter must be provided.");
        }
    }

    public async Task<bool> AddWorkspace(Workspace workspace)
    {
        workspace.CreatedAt = DateTime.Now;
        workspace.UpdatedAt = DateTime.Now;
        await _context.Workspaces.AddAsync(workspace);
        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateWorkspace(int workspaceId, Workspace workspace)
    {
        workspace.UpdatedAt = DateTime.Now;
        _context.Workspaces.Update(workspace);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteWorkspace(int workspaceId)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId);
        if (workspace == null || workspace.IsDeleted)
        {
            return false;
        }
        workspace.IsDeleted = true;
        workspace.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> RestoreWorkspace(int workspaceId)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId);
        if (workspace == null || !workspace.IsDeleted)
        {
            return false;
        }
        workspace.IsDeleted = false;
        workspace.UpdatedAt = DateTime.Now;
        return await SaveChangesAsync();
    }

    public async Task<bool> HardDeleteWorkspace(int workspaceId)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId);
        if (workspace == null || !workspace.IsDeleted)
        {
            return false;
        }
        _context.Workspaces.Remove(workspace);
        return await SaveChangesAsync();
    }
}
