using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Dto.Researchers;

public class ResearcherCreateDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
    public string Name { get; set; }

    [StringLength(100, ErrorMessage = "La spécialité ne doit pas dépasser 100 caractères.")]
    public string? Specialty { get; set; }
}