using DaraAds.Application.Services.Chat.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Chat.Interfaces
{
    public interface IChatService
    {
        Task<Get.Response> GetChats(Get.Request request, CancellationToken cancellationToken);

        Task Save(Save.Request request, CancellationToken cancellationToken);
    }
}
