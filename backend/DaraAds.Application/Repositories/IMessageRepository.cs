using DaraAds.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface IMessageRepository : IRepository<Message, long>
    {
        public Task SaveMessage(long chatId, string Text, CancellationToken cancellationToken);  
    }
}
