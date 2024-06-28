using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;
using SmartsearchApi.DTO;
using SmartsearchApi.Entities;

namespace SmartsearchApi.Controllers
{
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
        public async Task<ActionResult<IEnumerable<PublicationDTO>>> GetPublications()
        {
            var publications = await _context.Publications.ToListAsync();
    
            var publicationDtos = publications.Select(p => new PublicationDTO(p));
            
            return Ok(publicationDtos);
    
        }

        // GET: api/Publication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(int id)
        {
            var publication = await _context.Publications.FindAsync(id);

            if (publication == null)
            {
                return NotFound();
            }

            return publication;
        }

        // PUT: api/Publication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublication(int id, Publication publication)
        {
            if (id != publication.Id)
            {
                return BadRequest();
            }

            _context.Entry(publication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Publication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicationDTO>> PostPublication(NewPublicationDTO newPublicationDto)
        {
            if (!_context.Projects.Any(p => p.Id == newPublicationDto.ProjectId))
            {
                return BadRequest("Project not found");
            }

            var project = _context.Projects.FirstOrDefault(p => p.Id == newPublicationDto.ProjectId);

            var publication = new Publication
            {
                Titre = newPublicationDto.Titre,
                Resume = newPublicationDto.Resume,
                ProjectId = newPublicationDto.ProjectId,
                Project = project,
                DateDePublication = newPublicationDto.DateDePublication
            };

            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();

            var publicationDTO = new PublicationDTO
            {
                Id = publication.Id,
                Titre = publication.Titre,
                Resume = publication.Resume,
                ProjectId = publication.ProjectId,
                DateDePublication = publication.DateDePublication
            };

            return CreatedAtAction("GetPublication", new { id = publication.Id }, publicationDTO);
        }

        // DELETE: api/Publication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(int id)
        {
            var publication = await _context.Publications.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            _context.Publications.Remove(publication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicationExists(int id)
        {
            return _context.Publications.Any(e => e.Id == id);
        }
    }
}
