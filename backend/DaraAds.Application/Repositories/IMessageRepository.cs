using DaraAds.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface IMessageRepository : IRepository<Message, long>
    {
        Task<IEnumerable<Message>> FindMessagesByChat(long chatId, CancellationToken cancellationToken);
    }
}
