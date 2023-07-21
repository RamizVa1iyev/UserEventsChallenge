using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using UserEventsChallenge.API.Entities.Abstract;
using UserEventsChallenge.API.Entities.Paging;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<T> : IQuery<T> where T : class,IEntity, new()
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,IIncludableQueryable<T, object>>? include = null);

    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                    int index = 0, int size = 10, bool enableTracking = true,
                                    CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity);

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}