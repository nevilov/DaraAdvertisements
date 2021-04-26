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
using DaraAds.Infrastructure.Helpers;
using DaraAds.Domain;
using Amazon.S3.Model;

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
            return await _context.Advertisements.Where(a => ids.Contains(a.CategoryId)).Skip(offset).Take(limit).ToListAsync(cancellationToken);
        }

        
        //Удалить метод
        public async Task<Domain.Advertisement> FindByIdWithUser(int id, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Include(a => a.OwnerUser)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Domain.Advertisement>> Search(Expression<Func<Domain.Advertisement, bool>> predicate, int limit, int offset, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Where(predicate)
                .OrderBy(x => x.Id)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedList<Domain.Advertisement>> GetPageByFilterSortSearch(GetPages.Request parameters, CancellationToken cancellationToken)
         {
             var ads = from advertisement in _context.Advertisements select advertisement;
 
             var isCategorySet = parameters.CategoryId != 0; 
             if (isCategorySet)
             {
                 ads = ads.Where(a => a.CategoryId == parameters.CategoryId);
             }
             
             ads = ads.Where(a=> 
                 a.Price >= parameters.MinPrice && 
                 a.Price <= parameters.MaxPrice &&
                 a.CreatedDate.Date >= parameters.MinDate &&
                 a.CreatedDate.Date <= parameters.MaxDate);
 
             
             SearchByTitleOrDescription(ref ads, parameters.SearchString);
             
             var sortAds = _sortHelper.ApplySort(ads, parameters.SortOrder);

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

        public async Task<IEnumerable<Domain.Advertisement>> FindUserAdvertisements(string userId, int limit, int offset, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Where(x => x.OwnerId == userId)
                .OrderBy(x => x.CreatedDate)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }
    }
}
