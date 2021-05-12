using DaraAds.API.Controllers;
using DaraAds.Application.Services.Mail.Interfaces;
using DaraAds.Infrastructure;
using DaraAds.Infrastructure.Consumers;
using DaraAds.Infrastructure.Mail;
using DaraAds.Infrastructure.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers().AddNewtonsoftJson();

            services.AddApplicationModule();

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
