using AutoMapper;
using SmartsearchApi.Dto.Researchers;
using SmartsearchApi.Models;

namespace SmartsearchApi.Mappings;

public class ResearcherMappingProfile: Profile
{
    public ResearcherMappingProfile()
    {
        CreateMap<Researcher, ResearcherDto>().ReverseMap();
        CreateMap<Researcher, ResearcherLightDto>().ReverseMap();
        CreateMap<Researcher, ResearcherCreateDto>().ReverseMap();
    }
}