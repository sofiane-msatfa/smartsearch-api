using SmartsearchApi.Data;
using SmartsearchApi.Models;
using SmartsearchApi.Repositories.AbstractRepository;

namespace SmartsearchApi.Repositories.Publications;

public class PublicationRepository(ApplicationDbContext context) : AbstractRepository<Publication>(context), IPublicationRepository
{
    
}