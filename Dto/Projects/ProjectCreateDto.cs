using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Dto.Projects;

public class ProjectCreateDto
{
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Title { get; set; }
    
    [StringLength(1500)]
    public string? Description { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    [Required]
    public long ManagerID { get; set; }
}