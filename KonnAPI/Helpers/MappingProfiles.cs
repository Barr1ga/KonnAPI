using AutoMapper;
using KonnAPI.Dto;
using KonnAPI.Models;

namespace KonnAPI.Helpers;

public class MappingProfiles : Profile {
    public MappingProfiles() {
        CreateMap<Contact, ContactDto>();
    }
}
