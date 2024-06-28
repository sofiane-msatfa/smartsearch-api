using SmartsearchApi.Repositories.Projects;
using SmartsearchApi.Repositories.Publications;
using SmartsearchApi.Repositories.Researchers;

namespace SmartsearchApi.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IResearcherRepository Researchers { get; }
    IProjectRepository Projects { get; }
    IPublicationRepository Publications { get; }
    
    Task CommitAsync();
}