using SmartsearchApi.Data;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Projects;

public class ProjectRepository(ApplicationDbContext context) : AbstractRepository<Project>(context), IProjectRepository
{

}