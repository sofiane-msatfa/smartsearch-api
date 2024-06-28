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
    public async Task<ActionResult<IEnumerable<PublicationDto>>> GetPublications()
    {
        var publications = await uof.Publications.GetPublicationsWithProjects();
        var publicationDtos = mapper.Map<IEnumerable<PublicationDto>>(publications);
        return Ok(publicationDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PublicationDto>> GetPublication(long id)
    {
        var publication = await uof.Publications.GetPublicationWithProjectById(id);
        if (publication == null)
        {
            return NotFound();
        }
        var publicationDto = mapper.Map<PublicationDto>(publication);
        return Ok(publicationDto);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<PublicationLightDto>> PutPublication(long id, PublicationLightDto publicationLightDto)
    {
        if (id != publicationLightDto.Id)
        {
            return BadRequest();
        }
        
        var publication = mapper.Map<Publication>(publicationLightDto);
        var updated = uof.Publications.Update(publication);
        await uof.CommitAsync();
        
        var publicationLightDtoUpdated = mapper.Map<PublicationLightDto>(updated);

        return Ok(publicationLightDtoUpdated);
    }
    
    [HttpPost]
    public async Task<ActionResult<PublicationLightDto>> PostPublication(PublicationCreateDto publicationCreateDto)
    {
        var publication = mapper.Map<Publication>(publicationCreateDto);
        var created = uof.Publications.Create(publication);
        await uof.CommitAsync();
        
        var publicationLightDtoCreated = mapper.Map<PublicationLightDto>(created);
        return Ok(publicationLightDtoCreated);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePublication(long id)
    {
        var publication = await uof.Publications.GetAsync(p => p.Id == id);
        if (publication == null)
        {
            return NotFound();
        }
        
        uof.Publications.Delete(publication);
        await uof.CommitAsync();
        
        return NoContent();
    }
}