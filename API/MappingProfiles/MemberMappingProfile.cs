using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.MappingProfiles;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<MemberUpdateDto, AppUser>();
    }
}