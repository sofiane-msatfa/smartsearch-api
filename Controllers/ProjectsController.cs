using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartsearchApi.Dto.Projects;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.UnitOfWork;

namespace SmartsearchApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProjectsController(IUnitOfWork uof, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects([FromQuery] ProjectFiltersDto? filter = null)
    {
        var projects = await uof.Projects.GetProjectsWithRelations(filter);
        var projectsDto = mapper.Map<IEnumerable<ProjectDto>>(projects);

        return Ok(projectsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(long id)
    {
        var project = await uof.Projects.GetProjectWithRelationsById(id);

        if (project == null)
        {
            return NotFound();
        }

        var projectDto = mapper.Map<ProjectDto>(project);

        return Ok(projectDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectLightDto>> PutProject(long id, ProjectLightDto projectLightDto)
    {
        if (id != projectLightDto.Id)
        {
            return BadRequest();
        }

        var project = mapper.Map<Project>(projectLightDto);
        var updated = uof.Projects.Update(project);
        await uof.CommitAsync();

        var projectLightDtoUpdated = mapper.Map<ProjectLightDto>(updated);

        return Ok(projectLightDtoUpdated);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectLightDto>> PostProject(ProjectCreateDto projectCreateDto)
    {
        var project = mapper.Map<Project>(projectCreateDto);
        var created = uof.Projects.Create(project);
        await uof.CommitAsync();

        var projectLightDto = mapper.Map<ProjectLightDto>(created);

        return Ok(projectLightDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProject(long id)
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