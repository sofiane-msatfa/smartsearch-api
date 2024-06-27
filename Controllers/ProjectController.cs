using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Entities;

namespace SmartsearchApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResearcherDTO>>> GetProjects()
    {
        var researcher = await _context.Researchers
            .Include(p => p.Projects)
            .AsNoTracking()
            .ToListAsync();

        var researcherDtos = researcher.Select(p => new ResearcherDTO(p, true));

        return Ok(researcherDtos);
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ResearcherDTO>> GetProject(long id)
    {
        var researcher = await _context.Researchers
            .Include(p => p.Projects)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        if (researcher == null) return NotFound();

        return new ResearcherDTO(researcher, true);
    }

    // PUT: api/Project/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(long id, Project project)
    {
        if (id != project.Id) return BadRequest();

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Project
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<NewProjectDTO>> PostProject(NewProjectDTO newProject)
    {
        var project = new Project(newProject);

        foreach (var researcherId in newProject.ResearcherIds)
        {
            var researcher = await _context.Researchers.AsTracking().SingleOrDefaultAsync(r => r.Id == researcherId);
            if (researcher == null) return BadRequest($"Researcher with id {researcherId} not found");
            project.Researchers.Add(researcher);
        }

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        newProject.ResearcherIds = project.Researchers.Select(r => r.Id).ToList();
        return CreatedAtAction("GetProject", new { id = project.Id }, newProject);
    }

    // DELETE: api/Project/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(long id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProjectExists(long id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}