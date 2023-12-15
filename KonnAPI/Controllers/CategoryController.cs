using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/category/[controller]")]
[ApiController]
public class CategoryController : Controller {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories(int id) {
        try {
            var categories = await _categoryRepository.GetWorkspaceCategories(id);

            if (categories == null) {
                return NoContent();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = categories.OrderByDescending(a => a.CreatedAt).ToList() });
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

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = categories.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
