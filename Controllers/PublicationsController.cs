using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartsearchApi.Dto.Publications;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.UnitOfWork;

namespace SmartsearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicationsController(IUnitOfWork uof, IMapper mapper): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publication>>> GetPublications()
    {
        var publications = await uof.Publications.GetAllAsync();
        return Ok(publications);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Publication>> GetPublication(long id)
    {
        var publication = await uof.Publications.GetAsync(p => p.Id == id);
        if (publication == null)
        {
            return NotFound();
        }
        return Ok(publication);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<PublicationDto>> PutPublication(long id, PublicationDto publicationDto)
    {
        if (id != publicationDto.Id)
        {
            return BadRequest();
        }
        
        var publication = mapper.Map<Publication>(publicationDto);
        var updated = uof.Publications.Update(publication);
        await uof.CommitAsync();
        
        var updatedDto = mapper.Map<PublicationDto>(updated);
        return Ok(updatedDto);
    }
    
    [HttpPost]
    public async Task<ActionResult<PublicationDto>> PostPublication(PublicationCreateDto publicationDto)
    {
        var publication = mapper.Map<Publication>(publicationDto);
        var created = uof.Publications.Create(publication);
        await uof.CommitAsync();
        
        var createdDto = mapper.Map<PublicationDto>(created);
        return Ok(createdDto);
    }
}