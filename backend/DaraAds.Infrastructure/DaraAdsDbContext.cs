using Microsoft.EntityFrameworkCore;
using DaraAds.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DaraAds.Application.Common;
using DaraAds.Infrastructure.DataAccess.EntitiesConfiguration;
using System;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace DaraAds.Infrastructure
{
    public class DaraAdsDbContext : IdentityDbContext<Identity.IdentityUser>
    {
        public DaraAdsDbContext(DbContextOptions<DaraAdsDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Advertisement> Advertisements { get; set; }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<Abuse> Abuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Advertisement>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.CreatedDate).IsRequired();
                builder.Property(x => x.UpdatedDate).IsRequired(false);
                builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
                builder.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
                builder.Property(x => x.Cover).IsRequired(false);
                builder.Property(x => x.Price).HasColumnType("Money");
                builder.Property(x => x.Status).IsRequired();
                builder.Property(x => x.RemovedDate).IsRequired(false);

                builder.HasOne(x => x.OwnerUser)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Abuse>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.AbuseText).IsRequired(false).HasMaxLength(1000);
            });
            
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            
            SeedIdentity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedIdentity(ModelBuilder modelBuilder)
        {
            var ADMIN_ROLE_ID = "7ca197bb-d569-4fb9-b214-7f719973050e";
            var ADMIN_ID = "e4266faa-8fc0-4972-bf1c-14533f1ccffd";
            var USER_ROLE_ID = "b09f2dce-4821-4cf3-aa27-37f9d920bc01";
            var MODERATOR_ROLE_ID = "E8E08651-ED1B-468E-A931-F73E2563CD85";

            modelBuilder.Entity<IdentityRole>(x =>
            {
                x.HasData(new IdentityRole[]
                {
                    new IdentityRole()
                    {
                        Id = ADMIN_ROLE_ID,
                        Name = RoleConstants.AdminRole,
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole()
                    {
                        Id = USER_ROLE_ID,
                        Name = RoleConstants.UserRole,
                        NormalizedName = "USER"
                    },
                    new IdentityRole()
                    {
                        Id = MODERATOR_ROLE_ID,
                        Name = RoleConstants.ModeratorRole,
                        NormalizedName = "MODERATOR"
                    }
                });
            });

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                Email = "admin",
                EmailConfirmed = true,
                NormalizedUserName = "ADMIN"
            };

            var domainAdminUser = new Domain.User
            {
                Id = ADMIN_ID,
                Username = "admin",
                Email = "admin",
                CreatedDate = DateTime.UtcNow,
                Name = "admin",
                LastName = "admin",
            };

            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");

            modelBuilder.Entity<Identity.IdentityUser>(x =>
            {
                x.HasData(adminUser);
            });

            modelBuilder.Entity<Domain.User>(x =>
            {
                x.HasData(domainAdminUser);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(x =>
            {
                x.HasData(new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                });
            });
        }
    }
             
}
