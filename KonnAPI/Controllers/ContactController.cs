using AutoMapper;
using KonnAPI.Constants;
using KonnAPI.Data;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;


[Route("api/contacts/[controller]")]
[ApiController]
public class ContactController : Controller
{
    private readonly DataContext _context;
    private readonly IContactRepository _contactRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IContactCategoryRepository _contactCategoryRepository;
    private readonly IMapper _mapper;

    public ContactController(IContactRepository contactRepository, IAddressRepository addressRepository, IContactCategoryRepository contactCategoryRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _addressRepository = addressRepository;
        _contactCategoryRepository = contactCategoryRepository;
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

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var newContact = _mapper.Map<Contact>(contact);

                var existingContact = await _contactRepository.GetContact(name: newContact.Name, email: newContact.Email);

                if (existingContact != null) return BadRequest(new MessageDto(Status.Error, "Contact with this name and email already exists"));

                if (!await _contactRepository.AddContact(workspaceId, newContact))
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact"));
                }

                if (contact.CategoryIds != null)
                {
                    List<ContactCategory> contactCategories = new List<ContactCategory>();
                    contact.CategoryIds.ForEach(async c =>
                    {
                        contactCategories.Add(_mapper.Map<ContactCategory>(c));
                    });

                    if (!await _contactCategoryRepository.AddContactCategories(contactCategories))
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact categories"));
                    }
                }

                if (contact.Addresses != null)
                {
                    List<Address> addresses = new List<Address>();
                    contact.Addresses.ForEach(async a =>
                    {
                        addresses.Add(_mapper.Map<Address>(a));
                    });

                    if (!await _addressRepository.AddAddresses(addresses))
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact addresses"));
                    }
                }

                await transaction.CommitAsync();
                return Created(
                    "contacts",
                    new MessageDto(Status.Success, "Successfully added contact")
                );
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact"));
            }
        }
    }
    #endregion
}
