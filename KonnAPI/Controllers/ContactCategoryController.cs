using AutoMapper;
using KonnAPI.Constants;
using KonnAPI.Dto;
using KonnAPI.Interfaces;
using KonnAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonnAPI.Controllers;

[Route("api/category/[controller]")]
[ApiController]
public class ContactCategoryController : Controller
{
    private readonly IContactCategoryRepository _contactCategoryRepository;
    private readonly IMapper _mapper;

    public ContactCategoryController(IContactCategoryRepository contactCategoryRepository, IMapper mapper)
    {
        _contactCategoryRepository = contactCategoryRepository;
        _mapper = mapper;
    }

    #region AddWorkspaceCategory
    [HttpPost("{workspaceId}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Workspace>> AddContactCategory([FromBody] ContactCategoryCreateDto contactCategory)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new MessageDto(Status.Error, "Validation failed"));
        }

        var newContactCategory = _mapper.Map<ContactCategory>(contactCategory);

        var existingContactCategory = await _contactCategoryRepository.GetContactCategory(contactId: newContactCategory.ContactId, categoryId: newContactCategory.CategoryId);
        if (existingContactCategory != null) return BadRequest(new MessageDto(Status.Error, "Category for this contact already exists"));

        try
        {
            if (!await _contactCategoryRepository.AddContactCategory(newContactCategory))
            {
                return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact category"));
            }

            return Created(
                "categories",
                new MessageDto(Status.Success, "Successfully added contact category")
            );
        }
        catch (Exception)
        {
            return StatusCode(500, new MessageDto(Status.Error, "Something went wrong while adding the contact category"));
        }
    }
    #endregion
}
