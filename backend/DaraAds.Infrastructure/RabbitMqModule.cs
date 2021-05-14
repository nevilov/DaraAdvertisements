using DaraAds.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DaraAds.Infrastructure
{
    public static class RabbitMqModule
    {
        public static IServiceCollection AddRabbitMqModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(conf =>
            {
                conf.AddConsumer<ImportExcelConsumer>();
                conf.AddConsumer<SendNotificationConsumer>();

                conf.UsingRabbitMq((context, c) =>
                {
                    c.Host(configuration.GetValue<string>("RabbitMq:Host"), host =>
                    {
                        host.Username(configuration.GetValue<string>("RabbitMq:Username"));
                        host.Password(configuration.GetValue<string>("RabbitMq:Password"));
                    });

                    c.ReceiveEndpoint("import_excel", e => e.ConfigureConsumer<ImportExcelConsumer>(context));
                    c.ReceiveEndpoint("send_notifications", e => e.ConfigureConsumer<SendNotificationConsumer>(context));
                });
            }).AddMassTransitHostedService();


            return services;
        }
    }
}
