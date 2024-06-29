using System.Linq.Expressions;

namespace SmartsearchApi.Repositories.AbstractRepository;

public interface IAbstractRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}