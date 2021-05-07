using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Domain.Shared;

namespace DaraAds.Application.Repositories
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        Task<TEntity> FindById(TId id, CancellationToken cancellationToken);

        Task Save(TEntity entity, CancellationToken cancellationToken);

        Task Delete(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<int> Count(CancellationToken cancellationToken);

        Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetPaged(Expression<Func<TEntity, bool>> predicate, int offset, int limit, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> ListFindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }

}
