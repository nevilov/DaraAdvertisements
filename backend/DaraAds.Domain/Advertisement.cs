using System.Collections.Generic;
using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{

    /// <summary>
    /// Сущность объявление
    /// </summary>
    public class Advertisement : MutableEntity<int>
    {
        /// <summary>
        /// Статусы объявления
        /// </summary>
        public enum Statuses
        {
            Created,
            Payed,
            Closed
        }
            
        /// <summary>
        /// Заголовок объявления
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание объявления
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Локация объявления
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Координата объявления (Широта)
        /// </summary>
        public decimal GeoLat { get; set; }

        /// <summary>
        /// Координата объявления (Долгота)
        /// </summary>
        public decimal GeoLon { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public Statuses Status { get; set; }
        
        /// <summary>
        /// Обложка объявления
        /// </summary>
        public string Cover { get; set; }
        
        /// <summary>
        /// Список картинок
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }

        /// <summary>
        /// Автор объявления
        /// </summary>
        public virtual User OwnerUser { get; set; }

        /// <summary>
        /// Id автора объявления
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Категория объявления
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Id категории
        /// </summary>
        public int CategoryId { get; set; }
    }
}
