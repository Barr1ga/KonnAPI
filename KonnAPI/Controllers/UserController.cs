using AutoMapper;
using KonnAPI.Constants;
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

    #region GetUser
    [HttpPost]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> AddUser([FromBody] UserCreateDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new MessageDto(Status.Error, "Validation failed"));
        }

        var newUser = _mapper.Map<User>(user);

        var existingUser = await _userRepository.GetUser(email: newUser.Email);
        if (existingUser != null) return BadRequest(new MessageDto(Status.Error, "User with this email already exists"));

        try
        {
            if (!await _userRepository.AddUser(newUser))
            {
                return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the user"));
            }

            return Created(
                "users",
                new MessageDto(Status.Success, "Successfully added user")
            );
        }
        catch (Exception)
        {
            return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the user"));
        }
    }
    #endregion

    // TODO: Implement other CRUD operations
}
