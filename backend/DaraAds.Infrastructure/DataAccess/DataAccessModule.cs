using DaraAds.Application.Repositories;
using DaraAds.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DaraAds.Infrastructure.DataAccess
{
    public static class DataAccessModule
    {
        public static IServiceCollection AddDataAccessModule(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DaraAdsDbContext>(p =>
            {
                p.UseNpgsql(connectionString).UseLazyLoadingProxies();
            });

             services
                .AddScoped<IAdvertisementRepository, AdvertisementRepository>()
                .AddScoped<IRepository<Domain.User, string>, Repository<Domain.User, string>>()
                .AddScoped<IRepository<Domain.Abuse, int>, Repository<Domain.Abuse, int>>()
                .AddScoped<IRepository<Domain.Image, string>, Repository<Domain.Image, string>>()
                .AddScoped<IFavoriteRepository, FavoriteRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IChatRepository, ChatRepository>()
                .AddScoped<IMessageRepository, MessageRepository>();


            return services;
        }

    }
}
