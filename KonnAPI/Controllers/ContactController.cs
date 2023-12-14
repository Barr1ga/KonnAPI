using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;


[Route("api/contacts/[controller]")]
[ApiController]
public class ContactController : Controller {
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public ContactController(IContactRepository contactRepository, IMapper mapper) {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts() {
        try {
            var contacts = await _contactRepository.GetAllContacts();

            if (contacts == null) {
                return NoContent();
            }

            var contactDtos = _mapper.Map<List<ContactDto>>(contacts);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = contactDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetWorkspaceContacts(int id) {
        var contacts = await _contactRepository.GetWorkspaceContacts(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = contacts.OrderByDescending(a => a.CreatedAt).ToList() });
    }
}
