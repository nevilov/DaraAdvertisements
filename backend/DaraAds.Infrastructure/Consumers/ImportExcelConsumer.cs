using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.Consumers
{
    public class ImportExcelConsumer : IConsumer<ImportExcelMessage>
    {
        private readonly IAdvertisementService _advertisementService;

        public ImportExcelConsumer(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task Consume(ConsumeContext<ImportExcelMessage> context)
        {
            var importMessage = context.Message;
            await _advertisementService.CreateByExcelConsumer(importMessage, new System.Threading.CancellationToken());
        }
    }
}
