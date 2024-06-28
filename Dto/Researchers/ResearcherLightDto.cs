using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Dto.Researchers;

public class ResearcherLightDto
{
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string? Specialty { get; set; }
}