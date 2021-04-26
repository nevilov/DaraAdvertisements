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

        Task<IEnumerable<Domain.Advertisement>> Search(Expression<Func<Domain.Advertisement, bool>> predicate, int limit, int offset, CancellationToken cancellationToken);

        Task<PagedList<Domain.Advertisement>> GetPageByFilterSortSearch(GetPages.Request parameters, CancellationToken cancellationToken);
        
        Task<IEnumerable<Domain.Advertisement>> FindUserAdvertisements(string userId, int limit, int offset, CancellationToken cancellationToken);
    }
}
