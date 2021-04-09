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
        public Task<Domain.Advertisement> FindByIdWithUser(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<Domain.Advertisement>> FindByCategory(int id, int limit, int offset, CancellationToken cancellationToken);

        public Task<IEnumerable<Domain.Advertisement>> Search(Expression<Func<Domain.Advertisement, bool>> predicate, int limit, int offset, CancellationToken cancellationToken);

        public Task<PagedList<Domain.Advertisement>> GetPageByFilterSortSearch(GetPages.Request parameters, CancellationToken cancellationToken);
        
        public Task<IEnumerable<Domain.Advertisement>> FindUserAdvertisements(string userId, int limit, int offset, CancellationToken cancellationToken);
    }
}
