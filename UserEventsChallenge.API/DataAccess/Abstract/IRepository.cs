using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using UserEventsChallenge.API.Entities.Abstract;
using UserEventsChallenge.API.Entities.Paging;

namespace Core.Persistence.Repositories;

public interface IRepository<T> : IQuery<T> where T : class,IEntity, new()
{
    T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                         int index = 0, int size = 10,
                         bool enableTracking = true);

    T Add(T entity);
    IEnumerable<T> AddRange(IEnumerable<T> entities);
    T Update(T entity);
    T Delete(T entity);
}