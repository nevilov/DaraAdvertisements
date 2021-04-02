using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DaraAds.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class ImageConfiguration :IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.ImageBlob).IsRequired();
            builder.Property(i => i.CreatedDate).IsRequired();
            builder.Property(i => i.UpdatedDate).IsRequired(false);
            builder.Property(i => i.RemovedDate).IsRequired(false);
        }
    }
}