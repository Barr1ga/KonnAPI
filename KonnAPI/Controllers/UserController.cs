using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/users/[controller]")]
[ApiController]
public class UserController : Controller {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() {
        try {
            var users = await _userRepository.GetAllUsers();

            if (users == null) {
                return NoContent();
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = users.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
