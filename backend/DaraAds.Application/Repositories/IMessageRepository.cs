using DaraAds.Domain;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface IMessageRepository : IRepository<Message, long>
    {
        Task<Message[]> FindByAdvertisementAndUsers(int advertisementId, string ownerId, string customerId = null);
    }
}
