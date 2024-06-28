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
            query = query.Where(p => p.Title.Contains(filter.Title.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(p => p.Description != null && p.Description.Contains(filter.Description.ToLower()));
        }
        
        if (filter.StartDateFrom != null)
        {
            query = query.Where(p => p.StartDate >= filter.StartDateFrom);
        }
            
        
        if (filter.EndDateBefore != null)
        {
            query = query.Where(p => p.EndDate <= filter.EndDateBefore);
        }
        
        if (!string.IsNullOrEmpty(filter.ManagerId))
        {
            query = query.Where(p => p.ManagerId == long.Parse(filter.ManagerId));
        }
        
        if (!string.IsNullOrEmpty(filter.ManagerName))
        {
            query = query.Where(p => p.Manager.Name.Contains(filter.ManagerName));
        }
        
        if (filter.ResearcherIds.Length > 0)
        {
            query = query.Where(p => p.Researchers.Any(r => filter.ResearcherIds.Contains(r.Id.ToString())));
        }
        
        if (filter.PublicationIds.Length > 0)
        {
            query = query.Where(p => p.Publications.Any(pub => filter.PublicationIds.Contains(pub.Id.ToString())));
        }
        
        return query;
    }
}