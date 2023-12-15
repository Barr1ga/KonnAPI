using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/category/[controller]")]
[ApiController]
public class CategoryController : Controller {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories(int id) {
        try {
            var categories = await _categoryRepository.GetWorkspaceCategories(id);

            if (categories == null) {
                return NoContent();
            }

            var contactDtos = _mapper.Map<List<CategoryDto>>(categories);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = contactDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetWorkspaceCategories(int id) {
        try {
            var categories = await _categoryRepository.GetWorkspaceCategories(id);

            if (categories == null) {
                return NoContent();
            }

            var contactDtos = _mapper.Map<List<CategoryDto>>(categories);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = contactDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
