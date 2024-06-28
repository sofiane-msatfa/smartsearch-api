using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Models;

public class Publication
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(1500)]
    public string? Summary { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Required]
    public long ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}