using DaraAds.Application.Services.Abuse.Implementations;
using DaraAds.Application.Services.Abuse.Interfaces;
using DaraAds.Application.Services.User.Implementations;
using DaraAds.Application.Services.User.Interfaces;
using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DaraAds.Application.Services.Advertisement.Implementations;
using DaraAds.Application.Services.Advertisement.Interfaces;
using DaraAds.API.Controllers;
using DaraAds.Infrastructure.DataAccess.Repositories;
using DaraAds.Application.Repositories;
using DaraAds.Infrastructure.Mail;
using DaraAds.Application.Services.Mail.Interfaces;

namespace DaraAds.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAdvertisementService, AdvertisementService>()
            .AddScoped<IAbuseService, AbuseService>();

            services
             .AddScoped<IAdvertisementRepository, AdvertisementRepository>()
             .AddScoped<IRepository<Domain.User, string>, Repository<Domain.User, string>>()
             .AddScoped<IRepository<Domain.Abuse, int>, Repository<Domain.Abuse, int>>();


            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();

            services
            .AddHttpContextAccessor();

            services.AddIdentity(Configuration);

            services.AddControllers();

            services.AddSwaggerModule();
                        
            //Our db
            services.AddDbContext<DaraAdsDbContext>(p =>
            {
                p.UseNpgsql(Configuration.GetConnectionString("PostgreDB"));
            });

            services.AddApplicationException(config => { config.DefaultErrorStatusCode = 500; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Init migrations
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DaraAdsDbContext>();
            db.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DaraAds.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseApplicationException();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
