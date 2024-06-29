using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Dto.Projects;

public class ProjectFiltersDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateTime? StartDateFrom { get; set; }
    [DataType(DataType.Date)]
    public DateTime? EndDateBefore { get; set; }
    public string ManagerId { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public string[] ResearcherIds { get; set; } = Array.Empty<string>();
    public string[] PublicationIds { get; set; } = Array.Empty<string>();
}