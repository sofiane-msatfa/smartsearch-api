using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Dto.Publications;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Publications;

public class PublicationRepository(ApplicationDbContext context)
    : AbstractRepository<Publication>(context), IPublicationRepository
{
    public async Task<IEnumerable<Publication>> GetPublicationsWithProjects(PublicationFiltersDto? filter)
    {
        var query = PublicationsWithProjects();
        var publications = await ApplyFilter(query, filter).ToListAsync();

        return publications;
    }

    public async Task<Publication?> GetPublicationWithProjectById(long id)
    {
        return await PublicationsWithProjects().FirstOrDefaultAsync(p => p.Id == id);
    }

    private IQueryable<Publication> PublicationsWithProjects()
    {
        return Context.Publications
            .Include(p => p.Project)
            // .ThenInclude(p => p.Manager)
            .AsNoTracking();
    }

    private IQueryable<Publication> ApplyFilter(IQueryable<Publication> query, PublicationFiltersDto? filter)
    {
        if (filter == null)
        {
            return query;
        }

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(p => p.Title.Contains(filter.Title.ToLower()));
        }

        if (!string.IsNullOrEmpty(filter.Summary))
        {
            query = query.Where(p => p.Summary != null && p.Summary.Contains(filter.Summary.ToLower()));
        }

        if (filter.DateFrom != null)
        {
            query = query.Where(p => p.Date >= filter.DateFrom);
        }

        if (filter.DateTo != null)
        {
            query = query.Where(p => p.Date <= filter.DateTo);
        }

        if (filter.ProjectId != null)
        {
            query = query.Where(p => p.ProjectId == filter.ProjectId);
        }

        return query;
    }
}