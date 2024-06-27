using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.Entities;

namespace SmartsearchApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResearcherController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ResearcherController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
    {
        var projects = await _context.Projects
            .Include(p => p.Researchers)
            .AsNoTracking()
            .ToListAsync();

        var projectDtOs = projects.Select(p => new ProjectDTO(p, true));

        return Ok(projectDtOs);
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(long id)
    {
        var project = await _context.Projects
            .Include(p => p.Researchers)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        if (project == null) return NotFound();

        return new ProjectDTO(project, true);
    }

    // PUT: api/Researcher/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutResearcher(long id, Researcher researcher)
    {
        if (id != researcher.Id) return BadRequest();

        _context.Entry(researcher).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ResearcherExists(id))
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

    // DELETE: api/Researcher/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResearcher(long id)
    {
        var researcher = await _context.Researchers.FindAsync(id);
        if (researcher == null) return NotFound();

        _context.Researchers.Remove(researcher);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ResearcherExists(long id)
    {
        return _context.Researchers.Any(e => e.Id == id);
    }
}