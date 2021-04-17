using DaraAds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface IChatRepository : IRepository<Chat, long>
    {
        public Task<IEnumerable<Chat>> GetChats(string userId, bool isSeller, CancellationToken cancellationToken);
    }
}
