using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaraAds.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Email).IsRequired().HasMaxLength(256);
            builder.Property(i => i.Avatar).IsRequired(false);
            builder.Property(i => i.LastName).IsRequired().HasMaxLength(256).IsUnicode();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(256).IsUnicode();
            builder.Property(i => i.Phone).IsRequired(false).HasMaxLength(12);
            builder.Property(i => i.Username).IsRequired().HasMaxLength(30);

            builder.Property(i => i.CreatedDate).IsRequired();
            builder.Property(i => i.UpdatedDate).IsRequired(false);
            builder.Property(i => i.RemovedDate).IsRequired(false);
        }
    }
}
