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

        public async Task CreateChat(Chat chat, CancellationToken cancellationToken)
        {
            await _context.Chats.AddAsync(chat, cancellationToken);
        }

        public async Task<IEnumerable<Chat>> GetChats(string userId, CancellationToken cancellationToken)
        {
            return await _context.Chats
                .Where(c => c.BuyerId == userId || c.Advertisement.OwnerId == userId)
                .ToListAsync(cancellationToken);
        }

        public Task<IEnumerable<Message>> GetMessages(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
