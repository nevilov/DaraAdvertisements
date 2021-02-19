using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.User.Interfaces
{
    public interface IClaimsAccessor
    {
        Task<IEnumerable<Claim>> GetCurrentClaims(CancellationToken cancellationToken);
    }
}