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
    public async Task<ActionResult<IEnumerable<Researcher>>> GetResearchers()
    {
        var researchers = await uof.Researchers.GetAllAsync();
        return Ok(researchers);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Researcher>> GetResearcher(long id)
    {
        var researcher = await uof.Researchers.GetAsync(r => r.Id == id);
        if (researcher == null)
        {
            return NotFound();
        }
        return Ok(researcher);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Researcher>> PutResearcher(long id, ResearcherDto researcherDto)
    {
        if (id != researcherDto.Id)
        {
            return BadRequest();
        }
        
        var researcher = mapper.Map<Researcher>(researcherDto);
        var updated = uof.Researchers.Update(researcher);
        await uof.CommitAsync();
        
        return CreatedAtAction(nameof(GetResearcher), new { id = updated.Id }, updated);
    }
    
    [HttpPost]
    public async Task<ActionResult<Researcher>> PostResearcher(ResearcherCreateDto researcherDto)
    {
        var researcher = mapper.Map<Researcher>(researcherDto);
        var created = uof.Researchers.Create(researcher);
        await uof.CommitAsync();

        return CreatedAtAction(nameof(GetResearcher), new { id = created.Id }, created);
    }
}