using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Mail.Interfaces
{
    public interface IMailService
    {
        Task Send(string recepient, string subject, string message, CancellationToken cancellationToken = default);
    }
}
