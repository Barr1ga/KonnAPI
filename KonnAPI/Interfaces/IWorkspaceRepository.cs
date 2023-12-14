using KonnAPI.Models;

namespace KonnAPI.Interfaces;

public interface IWorkspaceRepository {
    Task<IEnumerable<Workspace>> GetAllWorkspaces();

    Task<IEnumerable<Workspace>> GetUserWorkspaces(int id);
}
