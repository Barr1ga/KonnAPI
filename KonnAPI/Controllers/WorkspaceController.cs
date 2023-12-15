using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/workspaces/[controller]")]
[ApiController]
public class WorkspaceController : Controller {

    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IMapper _mapper;

    public WorkspaceController(IWorkspaceRepository workspaceRepository, IMapper mapper) {
        _workspaceRepository = workspaceRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAllWorkspaces() {
        try {
            var workspaces = await _workspaceRepository.GetAllWorkspaces();

            if (workspaces == null) {
                return NoContent();
            }

            var workspaceDtos = _mapper.Map<List<WorkspaceDto>>(workspaces);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = workspaceDtos.OrderByDescending(a => a.CreatedAt).ToList() });
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

            var workspaceDtos = _mapper.Map<List<WorkspaceDto>>(workspaces);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = workspaceDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
