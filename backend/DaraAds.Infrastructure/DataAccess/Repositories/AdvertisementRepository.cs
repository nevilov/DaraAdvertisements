using DaraAds.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Helpers;
using DaraAds.Application.Services.Advertisement.Contracts;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class AdvertisementRepository : Repository<Domain.Advertisement, int>, IAdvertisementRepository
    {
        private readonly ISortHelper<Domain.Advertisement> _sortHelper;
        public AdvertisementRepository(DaraAdsDbContext context, ISortHelper<Domain.Advertisement> sortHelper) : base(context)
        {
            _sortHelper = sortHelper;
        }


        public async Task<IEnumerable<Domain.Advertisement>> FindAdvertisementsByCategoryIds(List<int> ids, int limit, int offset, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Where(a => ids.Contains(a.CategoryId) && (a.Status.Equals(Domain.Advertisement.Statuses.Created)))
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }
   

        public async Task<PagedList<Domain.Advertisement>> GetPageByFilterSortSearch(GetPages.Request parameters, List<int> ids, CancellationToken cancellationToken)
         {
             var ads = _context.Advertisements.AsQueryable();
 
             var isCategorySet = parameters.CategoryId != 0; 
             if (isCategorySet)
             {
                 ads = ads.Where(a => ids.Contains(a.CategoryId));
             }
             
             
             ads = ads.Where(a=> 
                 a.Price >= parameters.MinPrice && 
                 a.Price <= parameters.MaxPrice &&
                 a.CreatedDate.Date >= parameters.MinDate &&
                 a.CreatedDate.Date <= parameters.MaxDate &&
                 a.Status.Equals(Domain.Advertisement.Statuses.Created));
 
             
             SearchByTitleOrDescription(ref ads, parameters.SearchString);
             
             var sortAds = _sortHelper.ApplySort(ads, parameters.SortBy, parameters.SortDirection);

             return await PagedList<Domain.Advertisement>.ToPagedListAsync(sortAds, parameters.Limit, parameters.Offset,
                 cancellationToken);
         }

        private static void SearchByTitleOrDescription(ref IQueryable<Domain.Advertisement> ads, string searchString)
        {
            if (!ads.Any() || string.IsNullOrWhiteSpace(searchString)) return;
            
            var lowerCaseSearchString = searchString.Trim().ToLower();
            
            ads = ads.Where(a => 
                a.Title.ToLower().Contains(lowerCaseSearchString) || 
                a.Description.ToLower().Contains(lowerCaseSearchString));
        }
        
        public async Task<PagedList<Domain.Advertisement>> FindUserAdvertisements(string userId, int limit, int offset, string sortBy, string sortDirection, CancellationToken cancellationToken)
        {
            var ads = _context.Advertisements.AsQueryable();

            ads = ads.Where(a => a.OwnerId == userId && a.Status.Equals(Domain.Advertisement.Statuses.Created));
            
            var sortAds = _sortHelper.ApplySort(ads, sortBy, sortDirection);
            
            return await PagedList<Domain.Advertisement>.ToPagedListAsync(sortAds, limit, offset,
                cancellationToken);
        }
    }
}
