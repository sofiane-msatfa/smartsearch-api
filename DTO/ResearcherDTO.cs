using SmartsearchApi.Entities;

namespace SmartsearchApi.Data;

public class ResearcherDTO
{
    public ResearcherDTO(Researcher researcher, bool includeProjects)
    {
        Id = researcher.Id;
        Name = researcher.Name;
        Specialty = researcher.Specialty;
        if (includeProjects) Projects = researcher.Projects.Select(p => new ProjectDTO(p, false)).ToList();
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }

    public IEnumerable<ProjectDTO> Projects { get; set; }
}