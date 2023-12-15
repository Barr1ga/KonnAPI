using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/addresses/[controller]")]
[ApiController]
public class AddressController : Controller {
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressController(IAddressRepository addressRepository, IMapper mapper) {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses() {
        try {
            var addresses = await _addressRepository.GetAllAddresses();

            if (addresses == null) {
                return NoContent();
            }

            var addressDtos = _mapper.Map<List<ContactDto>>(addresses);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = addressDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Address>>> GetContactAddresses(int id) {
        try {
            var addresses = await _addressRepository.GetContactAddresses(id);

            if (addresses == null) {
                return NoContent();
            }

            var addressDtos = _mapper.Map<List<ContactDto>>(addresses);

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(new { data = addressDtos.OrderByDescending(a => a.CreatedAt).ToList() });
        } catch (Exception ex) {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
