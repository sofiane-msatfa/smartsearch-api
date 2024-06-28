using System.ComponentModel.DataAnnotations;
using SmartsearchApi.Dto.Projects;

namespace SmartsearchApi.Dto.Researchers;

public class ResearcherDto
{
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string? Specialty { get; set; }
    
    public ICollection<ProjectLightDto> Projects { get; set; } = new List<ProjectLightDto>();
    
    public ICollection<ProjectLightDto> ManagedProjects { get; set; } = new List<ProjectLightDto>();
}