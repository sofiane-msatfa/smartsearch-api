using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Researchers;

public class ResearcherRepository(ApplicationDbContext context) : AbstractRepository<Researcher>(context), IResearcherRepository
{
    
    private IQueryable<Researcher> ResearchersWithProjects()
    {
        return Context.Researchers
            .Include(r => r.Projects)
            .Include(r => r.ManagedProjects)
            .AsNoTracking();
    }

    public async Task<IEnumerable<Researcher>> GetResearchersWithProjects()
    {
        return await ResearchersWithProjects().ToListAsync();
    }
    
    public async Task<Researcher?> GetResearcherWithProjectsById(long id)
    {
        return await ResearchersWithProjects().FirstOrDefaultAsync(r => r.Id == id);
    }
}