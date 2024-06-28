using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartsearchApi.Dto.Projects;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.UnitOfWork;

namespace SmartsearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IUnitOfWork uof, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        var projects = await uof.Projects.GetAllAsync();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(long id)
    {
        var project = await uof.Projects.GetAsync(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Project>> PutProject(long id, ProjectDto projectDto)
    {
        if (id != projectDto.Id)
        {
            return BadRequest();
        }

        var project = mapper.Map<Project>(projectDto);
        var updated = uof.Projects.Update(project);
        await uof.CommitAsync();

        return CreatedAtAction(nameof(GetProject), new { id = updated.Id }, updated);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(ProjectCreateDto projectDto)
    {
        var project = mapper.Map<Project>(projectDto);
        var created = uof.Projects.Create(project);
        await uof.CommitAsync();

        return CreatedAtAction(nameof(GetProject), new { id = created.Id }, created);
    }

    [HttpDelete]
    public async Task<ActionResult<ProjectDto>> DeleteProject(long id)
    {
        var project = await uof.Projects.GetAsync(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        uof.Projects.Delete(project);
        await uof.CommitAsync();

        return NoContent();
    }
}