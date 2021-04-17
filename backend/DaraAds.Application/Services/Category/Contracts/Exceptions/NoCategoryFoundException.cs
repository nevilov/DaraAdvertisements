using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Category.Contracts.Exceptions
{
    public class NoCategoryFoundException : NotFoundException
    {
        public NoCategoryFoundException(string message) : base(message)
        {
        }
    }
}
