using SmartsearchApi.Entities;

namespace SmartsearchApi.Data;

public class ProjectDTO
{
    public ProjectDTO(Project project, bool includeResearchers)
    {
        Id = project.Id;
        Title = project.Title;
        Description = project.Description;
        if (includeResearchers) Researchers = project.Researchers.Select(r => new ResearcherDTO(r, false)).ToList();
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public IEnumerable<ResearcherDTO> Researchers { get; set; }
}