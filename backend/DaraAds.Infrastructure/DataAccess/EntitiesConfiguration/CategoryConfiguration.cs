using System.Collections.Generic;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaraAds.Infrastructure.DataAccess.EntitiesConfiguration
{
    
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private Category[] categories = new Category[]
        {
            new Category
            {
                Id = 1,
                Name = "Транспорт",
            },
            new Category()
            {
                Id = 2,
                Name = "Автомобили",
                ParentCategoryId = 1,
            },
            new Category
            {
                Id = 3,
                Name = "Мотоциклы",
                ParentCategoryId = 1,
            },
            new Category
            {
                Id = 4,
                Name = "Спецтехника",
                ParentCategoryId = 1,
            },
            new Category
            {
                Id = 5,
                Name = "Запчасти",
                ParentCategoryId = 1,
            },
            new Category
            {
                Id = 6,
                Name = "Недвижимость",
            },
            new Category()
            {
                Id = 7,
                Name = "Квартиры",
                ParentCategoryId = 6,
            },
            new Category
            {
                Id = 8,
                Name = "Дома",
                ParentCategoryId = 6,
            },
            new Category
            {
                Id = 9,
                Name = "Новостройки",
                ParentCategoryId = 6,
            },
            new Category
            {
                Id = 10,
                Name = "Гаражи",
                ParentCategoryId = 6,
            },
            new Category
            {
                Id = 11,
                Name = "Участки",
                ParentCategoryId = 6,
            },
            new Category
            {
                Id = 12,
                Name = "Бытовая Техника",
            },
            new Category
            {
                Id = 13,
                Name = "Аудио и видео",
                ParentCategoryId = 12,
            },
            new Category
            {
                Id = 14,
                Name = "Игры, приставки",
                ParentCategoryId = 12,
           },
            new Category
            {
                Id = 15,
                Name = "Компьютеры",
                ParentCategoryId = 12,
            },
            new Category
            {
                Id = 16,
                Name = "Ноутбуки",
                ParentCategoryId = 12,
            },
            new Category
            {
                Id = 17,
                Name = "Телефоны, планшеты",
                ParentCategoryId = 12,
            },
            new Category
            {
                Id = 18,
                Name = "Фототехника",
                ParentCategoryId = 12,
            },
            new Category
            {
                Id = 19,
                Name = "Животные",
            },
            new Category
            {
                Id = 20,
                Name = "Собаки",
                ParentCategoryId = 19,
            },
            new Category
            {
                Id = 21,
                Name = "Кошки",
                ParentCategoryId = 19,
            },
            new Category
            {
                Id = 22,
                Name = "Птицы",
                ParentCategoryId = 19,
            },
            new Category
            {
                Id = 23,
                Name = "Аквариум",
                ParentCategoryId = 19,
            },
            new Category
            {
                Id = 24,
                Name = "Товары для животных",
                ParentCategoryId = 19,
            },
        };
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(256).IsUnicode();
            
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.UpdatedDate).IsRequired(false);
            builder.Property(c => c.RemovedDate).IsRequired(false);

            // builder
            //     .HasMany(c => c.ChildCategories)
            //     .WithOne(c => c.ParentCategory)
            //     .HasForeignKey(c => c.ParentCategoryId);
            
            builder.HasData(categories);
        }
    }
}