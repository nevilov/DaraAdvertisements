using Amazon.S3;
using DaraAds.Application.Services.S3.Interfaces;
using DaraAds.Infrastructure.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace DaraAds.Infrastructure
{
    public static class S3Module
    {
        public static IServiceCollection AddS3(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddTransient<IS3Service, S3Service>();
            services.Configure<S3BucketSettings>(configuration.GetSection("S3BucketSettings"))
                .AddSingleton(x => x.GetRequiredService<IOptions<S3BucketSettings>>().Value);
            
            return services;
        }
    }
}