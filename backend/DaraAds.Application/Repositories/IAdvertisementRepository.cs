using DaraAds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface IAdvertisementRepository: IRepository<Domain.Advertisement, int>
    {
        public Task<Domain.Advertisement> FindByIdWithUser(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<Domain.Advertisement>> FindByCategory(int id, int limit, int offset, CancellationToken cancellationToken);
    }
}
