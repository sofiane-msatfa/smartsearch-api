namespace SmartsearchApi.Entities;

/**
 * * - Titre (char)
 * - Résumé (text)
 * - Projet associé (foreign key vers Projet de Recherche)
 * - Date de publication (date)
 */
public class Publication
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public DateTime PublicationDate { get; set; }
    public Project AssociatedProject { get; }
}