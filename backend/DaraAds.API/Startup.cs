using DaraAds.Application;
using DaraAds.Application.Services.Abuse.Implementations;
using DaraAds.Application.Services.Abuse.Interfaces;
using DaraAds.Application.Services.User.Implementations;
using DaraAds.Application.Services.User.Interfaces;
using DaraAds.Infrastructure;
using DaraAds.Infrastructure.DataAccess;
using DaraAds.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using DaraAds.Application.Services.Advertisement.Implementations;
using DaraAds.Application.Services.Advertisement.Interfaces;

namespace DaraAds.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserServiceV1>();
            services.AddScoped<IAdvertisementService, AdvertisementServiceV1>();
            services.AddScoped<IAbuseService, AbuseServiceV1>();

            services
             .AddScoped<InMemoryRepository>()
             .AddScoped<IRepository<Domain.Advertisement, int>>(sp => sp.GetService<InMemoryRepository>())
             .AddScoped<IRepository<Domain.User, int>>(sp => sp.GetService<InMemoryRepository>())
             .AddScoped<IRepository<Domain.Abuse, int>>(sp => sp.GetService<InMemoryRepository>());

            services
            .AddHttpContextAccessor()
            .AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName.Replace("+", "_"));
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DaraAds.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n
                        Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            //JWT-token settings
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
                    };
                });

            //Our db
            services.AddDbContext<DaraAdsDbContext>(p =>
            {
                p.UseNpgsql(Configuration.GetConnectionString("PostgreDB"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DaraAds.API v1"));
            }

            app.UseHttpsRedirection();

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
