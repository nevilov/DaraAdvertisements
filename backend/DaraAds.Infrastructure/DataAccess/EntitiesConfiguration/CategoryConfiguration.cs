using System.Collections.Generic;
using System.IO;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace DaraAds.Infrastructure.DataAccess.EntitiesConfiguration
{
    
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        { 
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(256).IsUnicode();
            
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.UpdatedDate).IsRequired(false);
            builder.Property(c => c.RemovedDate).IsRequired(false);

            builder
                .HasMany(c => c.ChildCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            // builder.HasData(SeedData());
        }

         private static IEnumerable<Category> SeedData()
         {
             using var r = new StreamReader((@"../DaraAds.Infrastructure/DataAccess/EntitiesConfiguration/Categories.json"));
             var json = r.ReadToEnd();
             var categories = JsonConvert.DeserializeObject<List<Category>>(json);
             return categories;
         }
    }
}