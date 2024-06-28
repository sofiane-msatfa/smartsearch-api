namespace SmartsearchApi.DTO;

public class NewPublicationDTO
{
    public string Titre { get; set; }
    public string Resume { get; set; }
    public int ProjectId { get; set; }
    public DateTime DateDePublication { get; set; }
}