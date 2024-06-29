using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartsearchApi.Models;

[Table("Researchers")]
public class Researcher(string name)
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = name;

    [MaxLength(100)]
    public string? Specialty { get; set; }

    public ICollection<Project> Projects { get; } = new List<Project>();

    public ICollection<Project> ManagedProjects { get; } = new List<Project>();
}