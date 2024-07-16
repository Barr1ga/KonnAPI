using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IWorkspaceRepository
{
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Workspace>> GetAllWorkspaces();
    Task<IEnumerable<Workspace>> GetUserWorkspaces(int userId);
    Task<Workspace?> GetUserWorkspace(int userId, int? workspaceId = null, string? workspaceName = null);
    Task<bool> AddWorkspace(Workspace workspace);
    Task<bool> UpdateWorkspace(int workspaceId, Workspace workspace);
    Task<bool> DeleteWorkspace(int workspaceId);
    Task<bool> RestoreWorkspace(int workspaceId);
    Task<bool> HardDeleteWorkspace(int workspaceId);
}
