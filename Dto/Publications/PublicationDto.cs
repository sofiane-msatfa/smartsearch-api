using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Dto.Publications;

public class PublicationDto
{
    public long Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(1500)]
    public string? Summary { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Required]
    public long ProjectId { get; set; }
}