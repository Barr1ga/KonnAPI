using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/workspaces/[controller]")]
[ApiController]
public class WorkspaceController : Controller {

    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkspaceController(IWorkspaceRepository workspaceRepository) {
        _workspaceRepository = workspaceRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAllWorkspaces() {
        try {
            var workspaces = await _workspaceRepository.GetAllWorkspaces();

            if (workspaces == null) {
                return NoContent();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = workspaces.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetUserWorkspaces(int id) {
        try {

            var workspaces = await _workspaceRepository.GetUserWorkspaces(id);

            if (workspaces == null) {
                return NoContent();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = workspaces.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
