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
        var categories = await _categoryRepository.GetWorkspaceCategories(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = categories.OrderByDescending(c => c.CreatedAt).ToList() });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetWorkspaceCategories(int id) {
        var categories = await _categoryRepository.GetWorkspaceCategories(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = categories.OrderByDescending(c => c.CreatedAt).ToList() });
    }
}
