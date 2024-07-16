using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Models;

namespace KonnAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Contact, ContactDto>();
        CreateMap<User, UserDto>();
        CreateMap<Workspace, WorkspaceDto>();
    }
}
