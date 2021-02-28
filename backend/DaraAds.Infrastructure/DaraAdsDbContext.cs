﻿using Microsoft.EntityFrameworkCore;
using DaraAds.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DaraAds.Application.Common;
using DaraAds.Infrastructure.DataAccess.EntitiesConfiguration;
using DaraAds.Domain;

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

            SeedIdentity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedIdentity(ModelBuilder modelBuilder)
        {
            var ADMIN_ROLE_ID = "7ca197bb-d569-4fb9-b214-7f719973050e";
            var ADMIN_ID = "e4266faa-8fc0-4972-bf1c-14533f1ccffd";
            var USER_ROLE_ID = "b09f2dce-4821-4cf3-aa27-37f9d920bc01";

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
                    }
                });
            });

            var passwordHasher = new PasswordHasher<Microsoft.AspNetCore.Identity.IdentityUser>();
            var adminUser = new Microsoft.AspNetCore.Identity.IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                Email = "admin",
                NormalizedUserName = "ADMIN"
            };

            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");

            modelBuilder.Entity<Identity.IdentityUser>(x =>
            {
                x.HasData(adminUser);
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
