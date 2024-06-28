using System.ComponentModel.DataAnnotations;
using SmartsearchApi.Dto.Publications;
using SmartsearchApi.Dto.Researchers;

namespace SmartsearchApi.Dto.Projects;

public class ProjectDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "The 'title' is required")]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(1500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The 'start date' is required")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The 'end date' is required")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    [Required(ErrorMessage = "The 'manager ID' is required")]
    public long ManagerId { get; set; }
    
    public ResearcherLightDto Manager { get; set; } = null!;
    
    public ICollection<ResearcherLightDto> Researchers { get; set; } = new List<ResearcherLightDto>();
    
    public ICollection<ResearcherLightDto> Publications { get; set; } = new List<ResearcherLightDto>();
}