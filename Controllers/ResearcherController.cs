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
    public async Task<ActionResult<IEnumerable<ResearcherDTO>>> GetResearchers()
    {
        var researcher = await _context.Researchers.Include(p => p.Projects).AsNoTracking().ToListAsync();

        var researcherDtos = researcher.Select(p => new ResearcherDTO(p, true));

        return Ok(researcherDtos);
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ResearcherDTO>> GetResearcher(long id)
    {
        var researcher = await _context.Researchers
            .Include(p => p.Projects)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        if (researcher == null) return NotFound();

        return new ResearcherDTO(researcher, true);
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
        public async Task<ActionResult<NewResearcherDTO>> PostResearcher(NewResearcherDTO newResearcher)
        {
            var researcher = new Researcher(newResearcher);

            foreach (var projectId in newResearcher.ProjectsId)
            {
                var project = await _context.Projects.AsTracking().SingleOrDefaultAsync(p => p.Id == projectId);
                if (project == null) return BadRequest($"Project with id {projectId} not found");
                researcher.Projects.Add(project);
            }

            _context.Researchers.Add(researcher);
            await _context.SaveChangesAsync();

            newResearcher.ProjectsId = researcher.Projects.Select(r => r.Id).ToList();
            return CreatedAtAction("GetResearcher", new { id = researcher.Id }, newResearcher);
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