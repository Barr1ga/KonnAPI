using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceController : Controller {

    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkspaceController(IWorkspaceRepository workspaceRepository) {
        _workspaceRepository = workspaceRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAllWorkspaces() {
        var workspaces = await _workspaceRepository.GetAllWorkspaces();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = workspaces.OrderByDescending(a => a.CreatedAt).ToList() });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetUserWorkspaces(int id) {
        var workspaces = await _workspaceRepository.GetUserWorkspaces(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = workspaces.OrderByDescending(a => a.CreatedAt).ToList() });
    }
}
