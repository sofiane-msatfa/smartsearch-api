using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Data;

namespace SmartsearchApi.Repositories.AbstractRepository;

public class AbstractRepository<T>(ApplicationDbContext context) : IAbstractRepository<T>
    where T : class
{
    protected readonly ApplicationDbContext Context = context;

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        var query = Context.Set<T>().AsQueryable();
        var entityType = Context.Model.FindEntityType(typeof(T));
        
        if (entityType == null)
        {
            return null;
        }

        // on récupère toutes les associations de navigation
        foreach (var property in entityType.GetNavigations())
        {
            Console.WriteLine($"Including property {property.Name}");
            query = query.Include(property.Name);
        }

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().AsNoTracking().ToListAsync();
    }

    public T Create(T entity)
    {
        Context.Set<T>().Add(entity);

        return entity;
    }

    public T Update(T entity)
    {
        Context.Set<T>().Update(entity);
        Context.Entry(entity).State = EntityState.Modified;

        return entity;
    }

    public T Delete(T entity)
    {
        Context.Set<T>().Remove(entity);

        return entity;
    }
}