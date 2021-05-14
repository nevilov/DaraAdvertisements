using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Helpers;
using DaraAds.Application.Services.Advertisement.Contracts;

namespace DaraAds.Application.Repositories
{
    public interface IAdvertisementRepository : IRepository<Domain.Advertisement, int>
    {
        Task<IEnumerable<Domain.Advertisement>> FindAdvertisementsByCategoryIds(List<int> ids, int limit, int offset, CancellationToken cancellationToken);

        Task<PagedList<Domain.Advertisement>> GetPageByFilterSortSearch(GetPages.Request parameters, List<int> ids, CancellationToken cancellationToken);
        
        Task<PagedList<Domain.Advertisement>> FindUserAdvertisements(string userId, int limit, int offset, string sortBy, string sortDirection, CancellationToken cancellationToken);
    }
}
