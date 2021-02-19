using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaraAds.Domain;

namespace DaraAds.Infrastructure
{
    public class DaraAdsDbContext : DbContext
    {
        public DaraAdsDbContext(DbContextOptions<DaraAdsDbContext> options) : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Abuse> Abuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>(builder =>
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

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

                builder.Property(x => x.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Abuse>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.AbuseText).IsRequired(false).HasMaxLength(1000);
            });

            base.OnModelCreating(modelBuilder);
        }
    }

            

             
}
