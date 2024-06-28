namespace SmartsearchApi.Dto.Projects;

public class ProjectFiltersDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string ManagerId { get; set; } = string.Empty;
    public string ResearcherId { get; set; } = string.Empty;
    public string PublicationId { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public string ResearcherName { get; set; } = string.Empty;
}