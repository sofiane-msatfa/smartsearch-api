using Microsoft.EntityFrameworkCore;

namespace SmartsearchApi.Entities;

[Keyless]
public class ProjectResearcher
{
    public long ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public long ResearcherId { get; set; }
    public Researcher Researcher { get; set; } = null!;
}