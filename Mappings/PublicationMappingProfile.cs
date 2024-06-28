using AutoMapper;
using SmartsearchApi.Models;
using SmartsearchApi.Dto.Publications;

namespace SmartsearchApi.Mappings;

public class PublicationMappingProfile: Profile
{
    public PublicationMappingProfile()
    {
        CreateMap<Publication, PublicationDto>().ReverseMap();
        CreateMap<Publication, PublicationCreateDto>().ReverseMap();
    }
}