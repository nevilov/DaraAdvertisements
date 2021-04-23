using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class ChatRepository : Repository<Chat, long>, IChatRepository
    {
        public ChatRepository(DaraAdsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Chat>> GetChats(string userId, bool isSeller, CancellationToken cancellationToken)
        {
            if (isSeller)
            {
                return await _context.Chats
                    .Where(c => c.Advertisement.OwnerId == userId)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                return await _context.Chats
                    .Where(c => c.BuyerId == userId)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
