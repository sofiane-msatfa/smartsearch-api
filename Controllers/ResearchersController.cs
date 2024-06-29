using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartsearchApi.Dto.Researchers;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.UnitOfWork;

namespace SmartsearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResearchersController(IUnitOfWork uof, IMapper mapper): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResearcherDto>>> GetResearchers()
    {
        var researchers = await uof.Researchers.GetResearchersWithProjects();
        var researchersDto = mapper.Map<IEnumerable<ResearcherDto>>(researchers);
        return Ok(researchersDto);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ResearcherDto>> GetResearcher(long id)
    {
        var researcher = await uof.Researchers.GetResearcherWithProjectsById(id);
        if (researcher == null)
        {
            return NotFound();
        }
        
        var researcherDto = mapper.Map<ResearcherDto>(researcher);
        return Ok(researcherDto);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ResearcherLightDto>> PutResearcher(long id, ResearcherLightDto researcherLightDto)
    {
        if (id != researcherLightDto.Id)
        {
            return BadRequest();
        }
        
        var researcher = mapper.Map<Researcher>(researcherLightDto);
        var updated = uof.Researchers.Update(researcher);
        await uof.CommitAsync();
        
        var researcherLightDtoUpdated = mapper.Map<ResearcherLightDto>(updated);

        return Ok(researcherLightDtoUpdated);
    }
    
    [HttpPost]
    public async Task<ActionResult<ResearcherLightDto>> PostResearcher(ResearcherCreateDto researcherCreateDto)
    {
        var researcher = mapper.Map<Researcher>(researcherCreateDto);
        var created = uof.Researchers.Create(researcher);
        await uof.CommitAsync();

        var researcherLightDtoCreated = mapper.Map<ResearcherLightDto>(created);

        return Ok(researcherLightDtoCreated);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResearcherLightDto>> DeleteResearcher(long id)
    {
        var researcher = await uof.Researchers.GetAsync(r => r.Id == id);
        if (researcher == null)
        {
            return NotFound();
        }
        
        uof.Researchers.Delete(researcher);
        await uof.CommitAsync();
        
        var researcherLightDto = mapper.Map<ResearcherLightDto>(researcher);
        return Ok(researcherLightDto);
    }
}