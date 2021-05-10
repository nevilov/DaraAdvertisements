using DaraAds.API.Controllers;
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
using DaraAds.Infrastructure;
using DaraAds.Infrastructure.Consumers;
using DaraAds.Infrastructure.DataAccess.Repositories;
using DaraAds.Infrastructure.Helpers;
using DaraAds.Infrastructure.Mail;
using DaraAds.Infrastructure.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

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
            .AddScoped<IAbuseService, AbuseService>()
            .AddScoped<IImageService, ImageService>()
            .AddScoped<IFavoriteService, FavoriteService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IFavoriteService, FavoriteService>()
            .AddScoped<IChatService, ChatService>()
            .AddScoped<IMessageService, MessageService>()
            .AddScoped<INotificationService, NotificationService>();

            services
             .AddScoped<IAdvertisementRepository, AdvertisementRepository>()
             .AddScoped<IRepository<Domain.User, string>, Repository<Domain.User, string>>()
             .AddScoped<IRepository<Domain.Abuse, int>, Repository<Domain.Abuse, int>>()
             .AddScoped<IRepository<Domain.Image, string>, Repository<Domain.Image, string>>()
             .AddScoped<IFavoriteRepository, FavoriteRepository>()
             .AddScoped<ICategoryRepository, CategoryRepository>()
             .AddScoped<IChatRepository, ChatRepository>()
             .AddScoped<IMessageRepository, MessageRepository>();

            services
                .AddScoped<ISortHelper<Domain.Advertisement>, SortHelper<Domain.Advertisement>>();

            services.AddHttpContextAccessor();

            services.AddScoped<IMailService, MailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddSignalRModule();

            services.AddS3(Configuration);

            services.AddIdentity(Configuration);
           

            services.AddMassTransit(conf =>
            {
                conf.AddConsumer<ImportExcelConsumer>();
                conf.AddConsumer<SendNotificationConsumer>();

                conf.UsingRabbitMq((context, c) =>
                {
                    c.Host(Configuration.GetValue<string>("RabbitMq:Host"), host =>
                    {
                        host.Username(Configuration.GetValue<string>("RabbitMq:Username"));
                        host.Password(Configuration.GetValue<string>("RabbitMq:Password"));
                    });

                    c.ReceiveEndpoint("import_excel", e => e.ConfigureConsumer<ImportExcelConsumer>(context));
                    c.ReceiveEndpoint("send_notifications", e => e.ConfigureConsumer<SendNotificationConsumer>(context));
                });
            }).AddMassTransitHostedService();

            services.AddSwaggerModule();

            //Our db
            services.AddDbContext<DaraAdsDbContext>(p =>
            {
                p.UseNpgsql(Configuration.GetConnectionString("PostgresDb")).UseLazyLoadingProxies();
            });

            services.AddApplicationException(config => { config.DefaultErrorStatusCode = 500; });

            services.ValidatorModule();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
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
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DaraAds.API v1"));

            app.UseHttpsRedirection();
            app.UseApplicationException();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://185.60.134.206:4200", "http://localhost:4200")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                .AllowCredentials();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/signalr/chat");
            });
        }
    }
}
