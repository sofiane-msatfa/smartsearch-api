using SmartsearchApi.Data;
using SmartsearchApi.Entities;

namespace SmartsearchApi.DTO;

public class PublicationDTO
{
    public PublicationDTO() { } 

    public PublicationDTO(Publication publication)
    {
        Id = publication.Id;
        Titre = publication.Titre;
        Resume = publication.Resume;
        ProjectId = publication.ProjectId;
        DateDePublication = publication.DateDePublication;
        Project = new ProjectDTO(publication.Project, true);
        
    }
    
    public int Id { get; set; }
    public string Titre { get; set; }
    public string Resume { get; set; }
    public int ProjectId { get; set; }
    public DateTime DateDePublication { get; set; }
    
    public ProjectDTO Project { get; set; }
}