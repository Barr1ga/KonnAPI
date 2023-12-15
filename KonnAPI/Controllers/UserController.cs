using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/users/[controller]")]
[ApiController]
public class UserController : Controller {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() {
        try {
            var users = await _userRepository.GetAllUsers();

            if (users == null) {
                return NoContent();
            }

            var userDtos = _mapper.Map<List<UserDto>>(users);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = userDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
