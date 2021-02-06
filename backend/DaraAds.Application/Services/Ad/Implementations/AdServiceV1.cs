using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Ad.Contracts;
using DaraAds.Application.Services.Ad.Interfaces;

namespace DaraAds.Application.Services.Ad.Implementations
{
    public sealed class AdServiceV1 : IAdService
    {
        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetPage.Response> GetPaged(GetPage.Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}