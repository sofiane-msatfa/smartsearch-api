namespace SmartsearchApi.Data;

public class NewProjectDTO
{
 public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<long> ResearcherIds { get; set; }
}