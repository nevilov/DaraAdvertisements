using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class MessageRepository : Repository<Message, long>, IMessageRepository
    {
        public MessageRepository(DaraAdsDbContext _context) : base(_context)
        {
        }

        public async Task<Message[]> FindByAdvertisementAndUsers(int advertisementId, string ownerId, string customerId = null)
        {
            var data =  _context.Messages
                .Where(m => m.AdvertisementId == advertisementId && m.Advertisement.OwnerId == ownerId);

            if (customerId != null)
            {
                data.Where(m => m.CustomerId == customerId);
            }
            return await data.ToArrayAsync();
        }
    }
}
