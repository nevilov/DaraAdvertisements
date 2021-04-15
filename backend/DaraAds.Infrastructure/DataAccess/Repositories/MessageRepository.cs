using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class MessageRepository : Repository<Message, long>, IMessageRepository
    {
        public MessageRepository(DaraAdsDbContext _context) : base(_context)
        {
        }

        public Task SaveMessage(long chatId, string Text, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
