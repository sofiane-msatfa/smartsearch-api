namespace SmartsearchApi.Controllers;

public class NewResearcherDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public List<long> ProjectsId { get; set; }
}