using SmartsearchApi.Data;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Researchers;

public class ResearcherRepository(ApplicationDbContext context) : AbstractRepository<Researcher>(context), IResearcherRepository
{
    
}