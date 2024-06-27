using SmartsearchApi.Controllers;

namespace SmartsearchApi.Entities;

/**
 * * - Nom (char)
 * - Spécialité (char)
 * - Liste des projets (many-to-many avec Projet de Recherche)
 */
public class Researcher
{
    public Researcher()
    {
    }

    public Researcher(NewResearcherDTO newResearcher)
    {
        Name = newResearcher.Name;
        Specialty = newResearcher.Specialty;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public List<Project> Projects { get; set; } = [];
}