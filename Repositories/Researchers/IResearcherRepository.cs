using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Researchers;

public interface IResearcherRepository: IAbstractRepository<Researcher>
{
    Task<IEnumerable<Researcher>> GetResearchersWithProjects();
    Task<Researcher?> GetResearcherWithProjectsById(long id);
}