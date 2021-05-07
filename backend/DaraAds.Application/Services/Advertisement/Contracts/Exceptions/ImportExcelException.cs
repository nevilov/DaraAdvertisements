using DaraAds.Domain.Shared.Exceptions;
namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public class ImportExcelException : ConflictException
    {
        public ImportExcelException(string message) : base(message)
        {
        }
    }
}
