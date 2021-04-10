using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class FavoriteRepository : Repository<Favorite, int>, IFavoriteRepository
    {
        public FavoriteRepository(DaraAdsDbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Favorite>> FindFavorites(string userId, int offset, int limit, CancellationToken cancellationToken)
        {
            return await _context.Favorites
                .Where(a => a.UserId == userId)
                .Include(a => a.Advertisement)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }
    }
}
