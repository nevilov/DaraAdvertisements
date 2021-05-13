using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DaraAds.Infrastructure.DataAccess
{
    public static class DataAccessModule
    {
        public static IServiceCollection AddDataAccessModule(this IServiceCollection services, IConfiguration configuration)
        {


            return services;
        }

    }
}
