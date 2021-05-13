using DaraAds.Application.Repositories;
using DaraAds.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class FavoriteRepository : Repository<Favorite, int>, IFavoriteRepository
    {
        private readonly ISortHelper<Favorite> _sortHelper;
        public FavoriteRepository(DaraAdsDbContext _context, ISortHelper<Favorite> sortHelper) : base(_context)
        {
            _sortHelper = sortHelper;
        }

        // public async Task<IEnumerable<Favorite>> FindFavorites(string userId, int offset, int limit, CancellationToken cancellationToken)
        // {
        //     return await _context.Favorites
        //         .Where(a => a.UserId == userId)
        //         .Include(a => a.Advertisement)
        //         .Skip(offset)
        //         .Take(limit)
        //         .ToListAsync(cancellationToken);
        // }

        public async Task<PagedList<Favorite>> FindFavorites(string userId, int offset, int limit, string sortBy, string sortDirection,
            CancellationToken cancellationToken)
        {
            
            var ads = _context.Favorites.AsQueryable();
            
            ads = ads.Where(a => a.UserId == userId);
            
            var sortAds = _sortHelper.ApplySort(ads, sortBy, sortDirection);
            
            return await PagedList<Favorite>.ToPagedListAsync(sortAds, limit, offset,
                cancellationToken);
        }
    }
}
