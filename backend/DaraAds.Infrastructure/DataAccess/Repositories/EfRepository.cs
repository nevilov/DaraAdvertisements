using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application;
using DaraAds.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public sealed class EfRepository<TEntity, TId> 
        : IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {

        private readonly DaraAdsDbContext _context;

        public EfRepository(DaraAdsDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> FindById(TId id, CancellationToken cancellationToken)
        {
            return await _context.FindAsync<TEntity>(new object[] {id}, cancellationToken);
        }

        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                await _context.AddAsync(entity, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var data = _context.Set<TEntity>();

            return await data.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            var data = _context.Set<TEntity>();

            return await data.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var data = _context.Set<TEntity>();

            return await data.OrderBy(e => e.Id).Take(limit).Skip(offset).ToListAsync(cancellationToken);
        }
    }
}
