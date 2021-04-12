using DaraAds.Application.SignalR.Interfaces;
using DaraAds.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace DaraAds.Infrastructure
{
    public static class SignalRModule
    {
        public static IServiceCollection AddSignalRModule(this IServiceCollection services)
        {
            services.AddSignalR();
            services
                .AddSingleton<IUserIdProvider, UserIdProvider>()
                .AddScoped<ISignalRService, SignalRService>();
               

            return services;
        }
    }
}
