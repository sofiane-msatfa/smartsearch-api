using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Publications;

public class PublicationRepository(ApplicationDbContext context)
    : AbstractRepository<Publication>(context), IPublicationRepository
{
    private IQueryable<Publication> PublicationsWithProjects()
    {
        return Context.Publications
            .Include(p => p.Project)
            // .ThenInclude(p => p.Manager)
            .AsNoTracking();
    }

    public async Task<IEnumerable<Publication>> GetPublicationsWithProjects()
    {
        return await PublicationsWithProjects().ToListAsync();
    }

    public async Task<Publication?> GetPublicationWithProjectById(long id)
    {
        return await PublicationsWithProjects().FirstOrDefaultAsync(p => p.Id == id);
    }
}