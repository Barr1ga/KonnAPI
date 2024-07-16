using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IWorkspaceRepository
{
    Task<IEnumerable<Workspace>> GetAllWorkspaces();

    Task<IEnumerable<Workspace>> GetUserWorkspaces(int userId);

    Task<bool> AddWorkspace(Workspace workspace);
    Task<bool> UpdateWorkspace(int workspaceId, Workspace workspace);
    Task<bool> DeleteWorkspace(int id);
    Task<bool> RestoreWorkspace(int id);
    Task<bool> HardDeleteWorkspace(int id);
}
