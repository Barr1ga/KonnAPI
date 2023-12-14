using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;


[Route("api/contacts/[controller]")]
[ApiController]
public class ContactController : Controller {
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository) {
        _contactRepository = contactRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts() {
        var contacts = await _contactRepository.GetAllContacts();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = contacts.OrderByDescending(a => a.CreatedAt).ToList() });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetWorkspaceContacts(int id) {
        var contacts = await _contactRepository.GetWorkspaceContacts(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = contacts.OrderByDescending(a => a.CreatedAt).ToList() });
    }
}
