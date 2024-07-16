using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/users/[controller]")]
[ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
[Produces("application/json")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsers();

            if (users == null)
            {
                return NoContent();
            }

            var userDtos = _mapper.Map<List<UserDto>>(users);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = userDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] UserCreateDto user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MessageDto("fail", "Validation failed"));
            }

            var newUser = _mapper.Map<User>(user);

            var existingUser = await _userRepository.GetUser(email: newUser.Email);
            if (existingUser != null) return BadRequest(new MessageDto("fail", "User with this email already exists"));

            if (!await _userRepository.AddUser(newUser))
            {
                ModelState.AddModelError("", "Something went wrong while adding the user");
                return StatusCode(500, ModelState);
            }

            return Created(
                "users",
                new MessageDto("success", "Successfully added user")
            );

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // TODO: Implement other CRUD operations
}
