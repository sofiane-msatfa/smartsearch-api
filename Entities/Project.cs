using SmartsearchApi.Data;

namespace SmartsearchApi.Entities;

/*
 * - Titre (char)
- Description (text)
- Date de début (date)
- Date de fin prévue (date)
- Chef de projet (foreign key vers Chercheur)
 */

public class Project
{
    public Project()
    {
    }

    public Project(NewProjectDTO newProject)
    {
        Title = newProject.Title;
        Description = newProject.Description;
        StartDate = newProject.StartDate;
        EndDate = newProject.EndDate;
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Researcher> Researchers { get; } = [];
}