using SmartsearchApi.Data;
using SmartsearchApi.Repositories.Projects;
using SmartsearchApi.Repositories.Publications;
using SmartsearchApi.Repositories.Researchers;

namespace SmartsearchApi.Repositories.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context): IUnitOfWork
{
    private IResearcherRepository? _researcherRepo;
    private IProjectRepository? _projectRepo;
    private IPublicationRepository? _publicationRepo;
    
    public IResearcherRepository Researchers
    {
        get
        {
            return _researcherRepo = _researcherRepo ?? new ResearcherRepository(context);
        }
    }
    
    public IProjectRepository Projects
    {
        get
        {
            return _projectRepo = _projectRepo ?? new ProjectRepository(context);
        }
    }
    
    public IPublicationRepository Publications
    {
        get
        {
            return _publicationRepo = _publicationRepo ?? new PublicationRepository(context);
        }
    }
    
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}