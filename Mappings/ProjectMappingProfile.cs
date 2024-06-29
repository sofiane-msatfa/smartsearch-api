using AutoMapper;
using SmartsearchApi.Models;
using SmartsearchApi.Dto.Projects;

namespace SmartsearchApi.Mappings;

public class ProjectMappingProfile: Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, ProjectLightDto>().ReverseMap();
        CreateMap<Project, ProjectCreateDto>().ReverseMap();
    }
}