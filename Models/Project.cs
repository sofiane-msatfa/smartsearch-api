using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Models;

public class Project
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(1500)]
    public string? Description { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    [Required]
    public long ManagerId { get; set; }
    public Researcher Manager { get; set; } = null!;
    
    public ICollection<Researcher> Researchers { get; } = new List<Researcher>();
    
    public ICollection<Publication> Publications { get; } = new List<Publication>();
}