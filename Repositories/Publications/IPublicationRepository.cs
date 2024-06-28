using SmartsearchApi.Dto.Publications;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Publications;

public interface IPublicationRepository : IAbstractRepository<Publication>
{
    Task<IEnumerable<Publication>> GetPublicationsWithProjects(PublicationFiltersDto? filter);
    Task<Publication?> GetPublicationWithProjectById(long id);
}