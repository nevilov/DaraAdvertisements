using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Identity.Contracts;

namespace DaraAds.Application.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetCurrentUserId(CancellationToken cancellationToken = default);
        Task<bool> IsInRole(string userId, string role, CancellationToken cancellationToken = default);
        Task<CreateUser.Response> CreateUser(CreateUser.Request request, CancellationToken cancellationToken = default);
        Task<CreateToken.Response> CreateToken(CreateToken.Request request, CancellationToken cancellationToken = default);
    }
}
