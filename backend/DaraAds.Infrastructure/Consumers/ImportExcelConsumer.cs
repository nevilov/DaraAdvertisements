using DaraAds.Application.Services.Advertisement.Contracts;
using MassTransit;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.Consumers
{
    public class ImportExcelConsumer : IConsumer<ImportExcelMessage>
    {
        public async Task Consume(ConsumeContext<ImportExcelMessage> context)
        {
            var importMessage = context.Message;
        }
    }
}
