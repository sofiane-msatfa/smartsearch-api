using System.Linq.Expressions;
using SmartsearchApi.Dto.Projects;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Projects;

public interface IProjectRepository: IAbstractRepository<Project>
{
    Task<IEnumerable<Project>> GetProjectsWithRelations(ProjectFiltersDto? filter);
    Task<Project?> GetProjectWithRelationsById(long id);
}