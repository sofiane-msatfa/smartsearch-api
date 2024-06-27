using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.DTO;
using SmartsearchApi.Entities;

namespace SmartsearchApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublicationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PublicationController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Publication
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publication>>> GetPublications()
    {
        return await _context.Publications.ToListAsync();
    }

    // GET: api/Publication/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Publication>> GetPublication(long id)
    {
        var publication = await _context.Publications.FindAsync(id);

        if (publication == null) return NotFound();

        return publication;
    }

    // PUT: api/Publication/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPublication(long id, Publication publication)
    {
        if (id != publication.Id) return BadRequest();

        _context.Entry(publication).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PublicationExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Publication
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<NewPublicationDTO>> PostPublication(NewPublicationDTO newPublicationDto)
    {
        var associatedProject = await _context.Projects.FindAsync(newPublicationDto.projectId);
        if (associatedProject == null)
        {
            return NotFound($"Project with ID {newPublicationDto.projectId} not found");
        }

        var publication = new Publication(newPublicationDto, associatedProject);

        _context.Publications.Add(publication);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPublication", new { id = publication.Id }, new PublicationDTO(publication));;
    }

    // DELETE: api/Publication/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublication(long id)
    {
        var publication = await _context.Publications.FindAsync(id);
        if (publication == null) return NotFound();

        _context.Publications.Remove(publication);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PublicationExists(long id)
    {
        return _context.Publications.Any(e => e.Id == id);
    }
}