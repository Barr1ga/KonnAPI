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
    public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses() {
        var addresses = await _userRepository.GetAllUsers();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = addresses.OrderByDescending(a => a.CreatedAt).ToList() });
    }
}
