using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Dto.Projects;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Projects;

public class ProjectRepository(ApplicationDbContext context) : AbstractRepository<Project>(context), IProjectRepository
{

    public async Task<IEnumerable<Project>> GetProjectsWithRelations(ProjectFiltersDto? filter)
    {
        var query = ProjectsWithRelations();
        var projects = await ApplyFilter(query, filter).ToListAsync();
        // return await ProjectsWithRelations().ToListAsync();
        return projects;
    }

    public async Task<Project?> GetProjectWithRelationsById(long id)
    {
        return await ProjectsWithRelations().FirstOrDefaultAsync(p => p.Id == id);
    }
    
    private IQueryable<Project> ProjectsWithRelations()
    {
        return Context.Projects
            .Include(p => p.Researchers)
            .Include(p => p.Manager)
            .Include(p => p.Publications)
            .AsNoTracking();
    }
    
    private IQueryable<Project> ApplyFilter(IQueryable<Project> query, ProjectFiltersDto? filter)
    {
        if (filter == null)
        {
            return query;
        }
        
        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(p => p.Title.Contains(filter.Title));
        }
        
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(p => p.Description != null && p.Description.Contains(filter.Description));
        }
        
        if (!string.IsNullOrEmpty(filter.StartDate))
        {
            // date after or equal to the given date
            // TODO: à documenter
            query = query.Where(p => p.StartDate >= DateTime.Parse(filter.StartDate));
        }
        
        if (!string.IsNullOrEmpty(filter.EndDate))
        {
            // date before or equal to the given date
            // TODO: à documenter
            query = query.Where(p => p.EndDate <= DateTime.Parse(filter.EndDate));
        }
        
        if (!string.IsNullOrEmpty(filter.ManagerId))
        {
            query = query.Where(p => p.ManagerId == long.Parse(filter.ManagerId));
        }
        
        if (!string.IsNullOrEmpty(filter.ResearcherId))
        {
            query = query.Where(p => p.Researchers.Any(r => r.Id == long.Parse(filter.ResearcherId)));
        }
        
        if (!string.IsNullOrEmpty(filter.PublicationId))
        {
            query = query.Where(p => p.Publications.Any(pub => pub.Id == long.Parse(filter.PublicationId)));
        }
        
        if (!string.IsNullOrEmpty(filter.ManagerName))
        {
            query = query.Where(p => p.Manager.Name.Contains(filter.ManagerName));
        }
        
        if (!string.IsNullOrEmpty(filter.ResearcherName))
        {
            query = query.Where(p => p.Researchers.Any(r => r.Name.Contains(filter.ResearcherName)));
        }
        
        return query;
    }
}