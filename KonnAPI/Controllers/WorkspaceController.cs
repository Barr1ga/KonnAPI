using AutoMapper;
using KonnAPI.Constants;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/workspaces/[controller]")]
[ApiController]
public class WorkspaceController : Controller
{

    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IMapper _mapper;

    public WorkspaceController(IWorkspaceRepository workspaceRepository, IContactRepository contactRepository, IMapper mapper)
    {
        _workspaceRepository = workspaceRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAllWorkspaces()
    {
        try
        {
            var workspaces = await _workspaceRepository.GetAllWorkspaces();

            if (workspaces == null)
            {
                return NoContent();
            }

            var workspaceDtos = _mapper.Map<List<WorkspaceDto>>(workspaces);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = workspaceDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetUserWorkspaces(int id)
    {
        try
        {

            var workspaces = await _workspaceRepository.GetUserWorkspaces(id);

            if (workspaces == null)
            {
                return NoContent();
            }

            var workspaceDtos = _mapper.Map<List<WorkspaceDto>>(workspaces);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = workspaceDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    #region AddWorkspace
    [HttpPost]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Workspace>> AddWorkspace([FromBody] int userId, [FromBody] WorkspaceCreateDto workspace)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new MessageDto(Status.Error, "Validation failed"));
        }

        var newWorkspace = _mapper.Map<Workspace>(workspace);

        var existingWorkspace = await _workspaceRepository.GetUserWorkspace(userId, workspaceName: newWorkspace.Name);
        if (existingWorkspace != null) return BadRequest(new MessageDto(Status.Error, "Workspace with this name already exists"));

        try
        {
            if (!await _workspaceRepository.AddWorkspace(newWorkspace))
            {
                return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the workspace"));
            }

            return Created(
                "workspaces",
                new MessageDto(Status.Success, "Successfully added workspace")
            );
        }
        catch (Exception)
        {
            return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the workspace"));
        }
    }
    #endregion



    // TODO: Implement other CRUD operations
}
