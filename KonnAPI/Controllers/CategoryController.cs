using AutoMapper;
using KonnAPI.Constants;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/category/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories(int id)
    {
        try
        {
            var categories = await _categoryRepository.GetWorkspaceCategories(id);

            if (categories == null)
            {
                return NoContent();
            }

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = categoryDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetWorkspaceCategories(int id)
    {
        try
        {
            var categories = await _categoryRepository.GetWorkspaceCategories(id);

            if (categories == null)
            {
                return NoContent();
            }

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = categoryDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    #region AddWorkspaceCategory
    [HttpPost("{workspaceId}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Workspace>> AddWorkspaceCategory([FromRoute] int workspaceId, [FromBody] CategoryCreateDto category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new MessageDto(Status.Error, "Validation failed"));
        }

        var newCategory = _mapper.Map<Category>(category);

        var existingCategory = await _categoryRepository.GetCategory(name: newCategory.Name);
        if (existingCategory != null) return BadRequest(new MessageDto(Status.Error, "Category with this name and email already exists"));

        try
        {
            if (!await _categoryRepository.AddCategory(workspaceId, newCategory))
            {
                return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the category"));
            }

            return Created(
                "categories",
                new MessageDto(Status.Success, "Successfully added category")
            );
        }
        catch (Exception)
        {
            return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the category"));
        }
    }
    #endregion
}
