using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : Controller {
    private readonly IAddressRepository _addressRepository;

    public AddressController(IAddressRepository addressRepository) {
        _addressRepository = addressRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses() {
        var addresses = await _addressRepository.GetAllAddresses();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = addresses.OrderByDescending(a => a.CreatedAt).ToList() });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetContactAddresses(int id) {
        var addresses = await _addressRepository.GetContactAddresses(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new { data = addresses.OrderByDescending(a => a.CreatedAt).ToList() });
    }
}
