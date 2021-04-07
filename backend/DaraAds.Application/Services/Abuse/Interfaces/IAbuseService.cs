using DaraAds.Application.Services.Abuse.Contracts;
using DaraAds.Application.Services.Advertisement.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Abuse.Interfaces
{
    public interface IAbuseService
    {
        Task<CreateAbuse.Response> CreateAbuse(CreateAbuse.Request request, CancellationToken cancellationToken);
        Task<GetAbusePages.Response> GetAbusePages(GetAbusePages.Request request, CancellationToken cancellationToken);
        Task CloseAbuse(CloseAbuse.Request request, CancellationToken cancellationToken);
    }
}
