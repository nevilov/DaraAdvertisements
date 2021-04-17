using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public sealed class AbuseNotFoundException : NotFoundException
    {
        public AbuseNotFoundException(int abuseId)
            : base($"Жалоба с ID [{abuseId}] не найдена.")
        {
        }
    }
}