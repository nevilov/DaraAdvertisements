using DaraAds.Application.Helpers;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Abuse.Implementations;
using DaraAds.Application.Services.Abuse.Interfaces;
using DaraAds.Application.Services.Advertisement.Implementations;
using DaraAds.Application.Services.Advertisement.Interfaces;
using DaraAds.Application.Services.Category.Implementations;
using DaraAds.Application.Services.Category.Interfaces;
using DaraAds.Application.Services.Chat.Implementations;
using DaraAds.Application.Services.Chat.Interfaces;
using DaraAds.Application.Services.Favorite.Implementations;
using DaraAds.Application.Services.Favorite.Interfaces;
using DaraAds.Application.Services.Image.Implementations;
using DaraAds.Application.Services.Image.Interfaces;
using DaraAds.Application.Services.Mail.Interfaces;
using DaraAds.Application.Services.Message.Implementations;
using DaraAds.Application.Services.Message.Interfaces;
using DaraAds.Application.Services.Notification.Implementations;
using DaraAds.Application.Services.Notification.Interfaces;
using DaraAds.Application.Services.User.Implementations;
using DaraAds.Application.Services.User.Interfaces;
using DaraAds.Infrastructure.DataAccess.Repositories;
using DaraAds.Infrastructure.Helpers;
using DaraAds.Infrastructure.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace DaraAds.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
               .AddScoped<IUserService, UserService>()
               .AddScoped<IAdvertisementService, AdvertisementService>()
               .AddScoped<IAbuseService, AbuseService>()
               .AddScoped<IImageService, ImageService>()
               .AddScoped<IFavoriteService, FavoriteService>()
               .AddScoped<ICategoryService, CategoryService>()
               .AddScoped<IFavoriteService, FavoriteService>()
               .AddScoped<IChatService, ChatService>()
               .AddScoped<IMessageService, MessageService>()
               .AddScoped<INotificationService, NotificationService>()
                .AddScoped<IMailService, MailService>();

            services
                .AddScoped<ISortHelper<Domain.Advertisement>, SortHelper<Domain.Advertisement>>()
                .AddScoped<ISortHelper<Domain.Favorite>, SortHelper<Domain.Favorite>>();

            return services;
        }
    }
}
