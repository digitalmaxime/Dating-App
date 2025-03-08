using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.MappingProfiles;

public class PhotoMappingProfile : Profile
{
    public PhotoMappingProfile()
    {
        CreateMap<Photo, PhotoDto>();
    }
}