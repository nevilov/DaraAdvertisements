using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Message>> FindMessagesByChat(long chatId, CancellationToken cancellationToken)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .ToListAsync(cancellationToken);
        }
    }
}
