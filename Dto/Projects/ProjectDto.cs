using System.ComponentModel.DataAnnotations;

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
    public long ManagerID { get; set; }
}