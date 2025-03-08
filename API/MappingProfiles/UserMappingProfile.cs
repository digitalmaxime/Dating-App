using API.Dtos;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault() != null ? src.Photos.FirstOrDefault()!.Url : null))
            .ForMember(dest => dest.Photos,
                opt => opt.MapFrom(src => src.Photos))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
    }
}