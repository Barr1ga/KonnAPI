using AutoMapper;
using KonnAPI.Constants;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;


[Route("api/contacts/[controller]")]
[ApiController]
public class ContactController : Controller
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public ContactController(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        try
        {
            var contacts = await _contactRepository.GetAllContacts();

            if (contacts == null)
            {
                return NoContent();
            }

            var contactDtos = _mapper.Map<List<ContactDto>>(contacts);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = contactDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetWorkspaceContacts(int id)
    {
        try
        {
            var contacts = await _contactRepository.GetWorkspaceContacts(id);

            if (contacts == null)
            {
                return NoContent();
            }

            var contactDtos = _mapper.Map<List<ContactDto>>(contacts);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { data = contactDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    #region AddWorkspaceContact
    [HttpPost("{workspaceId}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Workspace>> AddWorkspaceContact([FromRoute] int workspaceId, [FromBody] ContactCreateDto contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new MessageDto(Status.Error, "Validation failed"));
        }

        var newContact = _mapper.Map<Contact>(contact);

        var existingContact = await _contactRepository.GetContact(name: newContact.Name, email: newContact.Email);
        if (existingContact != null) return BadRequest(new MessageDto(Status.Error, "Contact with this name and email already exists"));

        try
        {
            if (!await _contactRepository.AddContact(workspaceId, newContact))
            {
                return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact"));
            }

            // TODO: add ContactCategories to contact
            // TODO: add Addresses to contact
            // TODO: add Socials to contact

            return Created(
                "contacts",
                new MessageDto(Status.Success, "Successfully added contact")
            );
        }
        catch (Exception)
        {
            return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact"));
        }
    }
    #endregion
}
