using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Projects;

public class ProjectRepository(ApplicationDbContext context) : AbstractRepository<Project>(context), IProjectRepository
{
    private IQueryable<Project> ProjectsWithRelations()
    {
        return Context.Projects
            .Include(p => p.Researchers)
            .Include(p => p.Manager)
            .Include(p => p.Publications)
            .AsNoTracking();
    }

    public async Task<IEnumerable<Project>> GetProjectsWithRelations()
    {
        return await ProjectsWithRelations().ToListAsync();
    }

    public async Task<Project?> GetProjectWithRelationsById(long id)
    {
        return await ProjectsWithRelations().FirstOrDefaultAsync(p => p.Id == id);
    }
}