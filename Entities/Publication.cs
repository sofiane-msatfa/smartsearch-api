namespace SmartsearchApi.Entities;

/**
 * * - Titre (char)
 * - Résumé (text)
 * - Projet associé (foreign key vers Projet de Recherche)
 * - Date de publication (date)
 */
public class Publication
{
  public int Id { get; set; }
  public string Titre { get; set; }
  public string Resume { get; set; }
  public int ProjectId { get; set; }
  public Project Project { get; set; }
  public DateTime DateDePublication { get; set; }
}